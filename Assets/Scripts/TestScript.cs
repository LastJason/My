using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    GameObject prefab;
    PoolBase pool;

    void Start()
    {
        prefab = Resources.Load<GameObject>("Cube");
    }

    bool screen = true;
    bool file = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---------Start-------");
            pool = PoolMgr.Instance.Create("mmmm", prefab, 3);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PoolMgr.Instance.Remove("mmmm");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            pool.Get(null, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject obj = GameObject.Find("Cube");
            pool.Put(obj);
        }
    }

}
