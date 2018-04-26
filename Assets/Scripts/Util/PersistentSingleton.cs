using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton Design Pattern with DontDestroyOnLoad
/// </summary>
/// <typeparam name="T"></typeparam>
public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if(_instance == null)
                {
                    GameObject singletonObj = new GameObject(typeof(T).ToString());
                    _instance = singletonObj.AddComponent<T>();
                }
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
}
