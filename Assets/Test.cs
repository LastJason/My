using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test:Singleton<Test>
{

    protected override void Init()
    {
        base.Init();
        Debug.LogError("1");
    }

    public void Demo()
    {
        Debug.LogError("test");
    }
}


