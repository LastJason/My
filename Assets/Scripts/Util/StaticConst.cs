using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticConst
{
    
    // 日志文件路径
    public static string LogDirectory = Application.dataPath + "/Log";
    // 日志文件个数
    public static int LogFileMaxNum = 3;

    // UI面板文件路径
    public static string PanelInfoPath = Application.dataPath + "/Scripts/Manager/UI/DataPanelInfo.json";
}
