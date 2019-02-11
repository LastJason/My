using System;
using System.Collections.Generic;
using UnityEngine;

public class Log : SingletonMono<Log>
{
    /*
     * #LOG_SCREEN  打印到屏幕
     * #LOG_FILE    打印到文件
     */

    private struct LogData
    {
        public string message;
        public string stackTrace;
        public LogType type;

        public LogData(string message, string stackTrace, LogType type)
        {
            this.message = message;
            this.stackTrace = stackTrace;
            this.type = type;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private bool isLogScreen = false;

    /// <summary>
    /// 
    /// </summary>
    private bool isLogFile = false;

    /// <summary>
    /// 
    /// </summary>
    private List<LogData> logList = new List<LogData>();

    private void Awake()
    {

    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLogScreen = true;
            isLogFile = true;
        }

        //if (isLogScreen)
        //{

        //}
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void OnDestroy()
    {

    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        logList.Add(new LogData(condition, stackTrace, type));
    }

    private void OnGUI()
    {
        if (!isLogScreen)
        {
            return;
        }

        //
        GUILayout.Window(10000, new Rect(0, Screen.height, Screen.width / 3, 0), WindowFunc, "1");
    }

    private void WindowFunc(int windowId)
    {

    }

}
