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


        //UIMgr.Instance.ShowPanel(PanelType.Task);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---------Start-------");
            UIMgr.Instance.ShowPanel(PanelType.MainMenu);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            UIMgr.Instance.ClosePanel();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UIMgr.Instance.ShowPanel(PanelType.Task);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject obj = GameObject.Find("Cube");
            pool.Put(obj);
        }
    }

}
