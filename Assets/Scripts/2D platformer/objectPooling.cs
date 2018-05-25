using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooling : MonoBehaviour
{
    public List<GameObject> prefabs;
    private List<GameObject> pool;
    private List<GameObject> inUse;

    [Range(0,15)]
    [Tooltip("specify the amout of doubles the script needs to create from the objects in pool list")]
    public int doubles;

    // Use this for initialization
    void Awake()
    {
        inUse = new List<GameObject>();
        pool = new List<GameObject>();


        for (int a = 0; a < doubles; a++)
        {
            for (int i = 0; i < prefabs.Count; i++)
            {
                GameObject temp = Instantiate(prefabs[i], Vector3.left * 10, Quaternion.identity, transform) as GameObject;

                pool.Add(temp);
                pool[i].SetActive(false);
            }
        }
        
    }

    public void stopUse()
    {
        if (inUse.Count <= 0)
        {
            Debug.LogError("inUse list is empty!");
            return;
        }
        inUse[0].SetActive(false);
        pool.Add(inUse[0]);
        inUse.RemoveAt(0);
    }

    public GameObject getNextItemInPool()
    {

        if (pool.Count <= 0)
        {
            Debug.LogError("Object pool is empty!");
            return null;
        }

        System.Random rnd = new System.Random();

        GameObject block = pool[rnd.Next(0, pool.Count-1)];
        pool.Remove(block);
        inUse.Add(block);

        block.SetActive(true);
        return block;
    }
}
