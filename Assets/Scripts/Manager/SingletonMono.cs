using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("APP");
                if (go == null)
                {
                    go = new GameObject("APP");
                    DontDestroyOnLoad(go);
                }

                _instance = go.GetComponent<T>();

                if (_instance == null)
                {
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
