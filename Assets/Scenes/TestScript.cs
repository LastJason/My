using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public TextAsset text;

    // Start is called before the first frame update
        int a = 1;
    void Start()
    {
        InvokeRepeating("Console", 5, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }

    }

    void Console()
    {
        Debug.LogError(a.ToString());
        a++;
    }
}
