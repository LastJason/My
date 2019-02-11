using System;
using System.Collections.Generic;
using UnityEngine;

public class LogE : SingletonMono<LogE>
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
    /// 输出屏幕
    /// </summary>
    public bool IsLogScreen { get; set; } = false;

    /// <summary>
    /// 输出日志
    /// </summary>
    public bool IsLogFile { get; set; } = false;

    /// <summary>
    /// 合并相同
    /// </summary>
    public bool IsLogCollapse { get; set; } = false;


    private List<LogData> logList = new List<LogData>();

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        logList.Add(new LogData(condition, stackTrace, type));
    }

    private void Update()
    {
        if (IsLogScreen)
        {

        }

        if (IsLogFile)
        {

        }

    }


    #region LogToScreen

    private Vector2 scrollPosition;

    private static Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {  
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };

    private void OnGUI()
    {
        if (!IsLogScreen)
        {
            return;
        }
        GUILayout.Window(123456, new Rect(0, 0, 250, 500), DrawWindow, "");
    }

    private void DrawWindow(int windowId)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (var i = 0; i < logList.Count; i++)
        {
            var log = logList[i];
            if (i > 0 && IsLogCollapse)
            {
                var previousMessage = logList[i - 1].message;

                if (log.message == previousMessage)
                {
                    continue;
                }
            }

            GUI.contentColor = logTypeColors[log.type];
            GUILayout.Label(log.message);
        }

        GUILayout.EndScrollView();
    }

    #endregion

    #region LogToFile

    //文件位置



    #endregion
}
