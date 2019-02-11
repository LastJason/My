using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Console", 5, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Test.Instance.Demo();
            LogE.Instance.IsLogScreen = true;
        }
    }

    void Console()
    {
        Debug.LogError("q");
        Debug.Log("w");
    }
}
