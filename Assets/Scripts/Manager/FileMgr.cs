using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

public class FileMgr : Singleton<FileMgr>
{
    /// <summary>
    /// 文件字典
    /// </summary>
    private Dictionary<string, FileData> fileDataDic = new Dictionary<string, FileData>();

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void Init()
    {
        InitFileData();
    }

    /// <summary>
    /// 初始化文件及文件夹设置
    /// </summary>
    private void InitFileData()
    {
        //添加文件
        fileDataDic.Add("log", new FileData(Tools.GetDataTime() + ".json", PathMgr.logDirectory, PathMgr.logMaxFile));

        if (fileDataDic.Count == 0) return;
        foreach (var item in fileDataDic)
        {
            string name = item.Value.name;
            string path = item.Value.path;
            int max = item.Value.pathmax;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            FileExistNum(max, path);
        }
    }

    private void FileExistNum(int max,string filedir)
    {
        if (!Directory.Exists(filedir)) return;

        try
        {
            string[] files = Directory.GetFiles(filedir);
            int offset = files.Length - max;
            for (int i = 0; i < offset-1; i++)
            {
                File.Delete(files[i]);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[EXCEPTION_FILEEXISTNUM]:" + e);
        }
    }

    /// <summary>
    /// 文件写入
    /// </summary>
    /// <param name="isAppend">是否保留原内容</param>
    private void Write(string filePath, string content, bool isAppend)
    {
        using (StreamWriter sw = new StreamWriter(filePath, isAppend, Encoding.UTF8))
        {
            sw.WriteLine(Tools.GetDataTime()+": "+content);
            sw.Flush();
            sw.Close();
        }
    }

    
    //----------------------------------------------------------------------------------------------------
    #region LogFile 日志写入

    /// <summary>
    /// 写日志
    /// </summary>
    /// <param name="log">日志内容</param>
    public void WriteLog(string log)
    {
        try
        {
            Write(fileDataDic["log"].path + "\\" + fileDataDic["log"].name,log,true);
            //Write(logFileName, log, true);
        }
        catch (System.Exception e)
        {
            Debug.LogError("[EXCEPTION_WRITELOG]:" + e);
        }
    }


    #endregion
    /// <summary>
    /// 文件数据结构
    /// </summary>
    private struct FileData
    {
        public string name;
        public string path;
        public int pathmax;

        public FileData(string name, string path, int pathmax) {
            this.name = name;
            this.path = path;
            this.pathmax = pathmax;
        }
    }

}
