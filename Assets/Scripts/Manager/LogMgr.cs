using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMgr : SingletonMono<LogMgr>
{
    //public methods 
    /// <summary>
    /// 开启打印到屏幕 若已经开启 则无操作
    /// </summary>
    /// <param name="isopen"></param>
    /// <param name="iscollapse"></param>
    /// <param name="priority"></param>
    public void LogToScreen(bool isopen, bool iscollapse, LogPriority priority)
    {
        IsLogScreen = isopen;
        _isLogScreenCollapse = iscollapse;
        logScreenPriority = priority;
    }

    /// <summary>
    /// 开启打印到文件 若已经开启 则无操作
    /// </summary>
    /// <param name="isopen"></param>
    /// <param name="iscollapse"></param>
    /// <param name="priority"></param>
    public void LogToFile(bool isopen, bool iscollapse, LogPriority priority)
    {
        IsLogFile = isopen;
        _isLogFileCollapse = iscollapse;
        logFilePriority = priority;
    }

    //----------------------------------------------------------------------------------------------------
    #region LogToScreen 开始->接收->绘制->是否去重->刷新

    /// <summary>
    /// 是否输出到屏幕
    /// </summary>
    private bool _isLogScreen = false;
    private bool IsLogScreen
    {
        get { return _isLogScreen; }
        set
        {
            if (value == _isLogScreen)
            {
                return;
            }
            _isLogScreen = value;
            InitScreen(_isLogScreen);
        }
    }

    /// <summary>
    /// 是否需要合并相同
    /// </summary>
    private bool _isLogScreenCollapse = false;

    /// <summary>
    /// 记录输出类型
    /// </summary>
    private LogPriority logScreenPriority = LogPriority.All;
    /// <summary>
    /// 开始记录初始化
    /// </summary>
    /// <param name="isStart"></param>
    private void InitScreen(bool isStart)
    {
        if (isStart)
        {
            logCollapseList.Clear();
            logScreenList.Clear();
            Application.logMessageReceived += HandleScreenLog;
        }
        else
            Application.logMessageReceived -= HandleScreenLog;
    }

    /// <summary>
    /// 所有记录列表
    /// </summary>
    /// <typeparam name="LogData"></typeparam>
    /// <returns></returns>
    private List<LogData> logScreenList = new List<LogData>();

    /// <summary>
    /// 不重复记录列表
    /// </summary>
    /// <typeparam name="LogData"></typeparam>
    /// <returns></returns>
    private List<LogData> logCollapseList = new List<LogData>();

    /// <summary>
    /// 滑动条
    /// </summary>
    private Vector2 scrollPosition;

    /// <summary>
    /// 处理接收消息记录   
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void HandleScreenLog(string condition, string stackTrace, LogType type)
    {
        //Log优先级是否达到标准
        int priority = (int)logPriorityDic[type];
        if (priority > (int)logScreenPriority)
        {
            return;
        }

        LogData log = new LogData(condition, stackTrace, type);
        logScreenList.Add(log);

        if (!logCollapseList.Contains(log))
        {
            logCollapseList.Add(log);
        }
    }

    /// <summary>
    /// 绘制
    /// </summary>
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
        var list = _isLogScreenCollapse ? logCollapseList : logScreenList;
        ShowLog(list);
        GUILayout.EndScrollView();

        if (GUILayout.Button("Clear"))
        {
            logCollapseList.Clear();
            logScreenList.Clear();
        }
    }

    private void ShowLog(List<LogData> list)
    {
        foreach (var item in list)
        {
            GUI.contentColor = logTypeColorsDic[item.type];
            GUILayout.Label(item.message);
        }
    }
    #endregion

    //----------------------------------------------------------------------------------------------------
    #region LogToFile 开始->接收->是否去重->写入

    /// <summary>
    /// 是否输出到文件
    /// </summary>
    private bool _isLogFile = false;
    private bool IsLogFile
    {
        get { return _isLogFile; }
        set
        {
            if (value == _isLogFile)
            {
                return;
            }
            _isLogFile = value;
            InitFile(_isLogFile);
        }
    }

    /// <summary>
    /// 是否需要合并相同
    /// </summary>
    private bool _isLogFileCollapse = true;

    /// <summary>
    /// 记录输出类型
    /// </summary>
    private LogPriority logFilePriority = LogPriority.All;

    /// <summary>
    /// 开始记录初始化
    /// </summary>
    /// <param name="isStart"></param>
    private void InitFile(bool isStart)
    {
        if (isStart)
        {
            logFileList.Clear();
            Application.logMessageReceived += HandleFileLog;
        }
        else
            Application.logMessageReceived -= HandleFileLog;
    }

    /// <summary>
    /// 所有记录列表
    /// </summary>
    /// <typeparam name="LogData"></typeparam>
    /// <returns></returns>
    private List<LogData> logFileList = new List<LogData>();

    /// <summary>
    /// 处理接收消息记录   
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void HandleFileLog(string condition, string stackTrace, LogType type)
    {
        Debug.LogError("HANDLE");
        //Log优先级是否达到标准
        int priority = (int)logPriorityDic[type];
        if (priority > (int)logFilePriority)
        {
            return;
        }

        LogData log = new LogData(condition, stackTrace, type);
        if (_isLogFileCollapse && logFileList.Contains(log))
        {
            return;
        }

        logFileList.Add(log);
        FileMgr.Instance.WriteLog(log.message);
    }

    #endregion

    //----------------------------------------------------------------------------------------------------
    #region LogData 日志数据结构体

    /// <summary>
    /// LogData
    /// </summary>
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
    /// 记录颜色类型
    /// </summary>
    /// <value></value>
    private Dictionary<LogType, Color> logTypeColorsDic = new Dictionary<LogType, Color> {
        { LogType.Assert, Color.white },
        { LogType.Error, Color.red },
        { LogType.Exception, Color.red },
        { LogType.Log, Color.white },
        { LogType.Warning, Color.yellow },
    };

    /// <summary>
    /// 记录优先级类型
    /// </summary>
    /// <value></value>
    private Dictionary<LogType, int> logPriorityDic = new Dictionary<LogType, int>{
        { LogType.Assert, 5 },
        { LogType.Warning, 4 },
        { LogType.Log, 3},
        { LogType.Error,2 },
        { LogType.Exception, 1 },
    };

    public enum LogPriority
    {
        Exception = 1,
        Error = 2,      //Exception + Error
        All = 3,        //Exception + Error + Log (not include Warning and Assert)
    }

    #endregion
}
