using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        LogMgr.Instance.LogToScreen(true, false, LogMgr.LogPriority.All);
        LogMgr.Instance.LogToFile(true, false, LogMgr.LogPriority.All);
    }

    bool screen = true;
    bool file = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---------Start-------");
            InvokeRepeating("Console", 5, 2);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            screen = !screen;
            LogMgr.Instance.LogToScreen(!screen, false, LogMgr.LogPriority.All);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            file = !file;
            LogMgr.Instance.LogToFile(!file, false, LogMgr.LogPriority.All);
        }
    }

    int a = 1;
    void Console()
    {
        Debug.LogError(a.ToString());
        a++;
        
    }
}
