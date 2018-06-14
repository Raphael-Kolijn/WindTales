using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class PlayCountPersistor
{
	private SaveData data;

	public PlayCountPersistor(string name)
	{
		Load(name);
	}

	public void IncreasePlayCount()
	{
		if (data.PlayCount.ContainsKey(DateTime.Today))
		{
			data.PlayCount[DateTime.Today] += 1;
		}
		else
		{
			data.PlayCount.Add(DateTime.Today, 1);
		}
		
		Save();
	}
	
	public int GetPlayCount()
	{		
		int playCount;
		if (data.PlayCount.TryGetValue(DateTime.Today, out playCount))
		{
			return playCount;
		}

		return 0;		
	}

	public int GetTotalPlayCount()
	{
		var playCount = 0;
		foreach (var i in data.PlayCount)
		{
			playCount += i.Value;
		}

		return playCount;
	}

	private void Save()
	{
		try
		{
			using (StreamWriter file = File.CreateText(Application.persistentDataPath + "/" + data.Name + ".json"))
			{
				Debug.Log("Saving file to: " + Application.persistentDataPath + data.Name);
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, data);
			}

			Debug.Log("File saved");
		}
		catch (IOException e)
		{
			Debug.LogError(e.ToString());
		}
	}

	private void Load(string name)
	{
		Debug.Log("Loading file...");
		try
		{
			using (StreamReader file = File.OpenText(Application.persistentDataPath + "/" + name + ".json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				SaveData loadedData = (SaveData) serializer.Deserialize(file, typeof(SaveData));
				this.data = loadedData;
				Debug.Log("File loaded succesfuly");
			}
		} 
		catch (IOException e)
		{
			data = new SaveData(name);
			Debug.Log("No file found. Creating new save object");
		}
	}
}

[Serializable]
class SaveData
{
	public string Name;
	public Dictionary<DateTime, int> PlayCount;

	public SaveData(String name)
	{
		this.Name = name;
		PlayCount = new Dictionary<DateTime, int>();	
	}
}