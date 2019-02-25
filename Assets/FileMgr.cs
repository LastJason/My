using UnityEngine;
using System.IO;
using System;
using System.Text;

public class FileMgr:Singleton<FileMgr>{

    private string logFileName;

    /// <summary>
    /// 初始化
    /// </summary>
    protected override void Init(){

        // log
        logFileName =PathMgr.logDirectory+"\\"+Tools.GetDataTime()+".txt";

    }

    public void WriteLog(string log){
        Debug.Log("```");
        try
        {
            Write(logFileName,log,true);
        }
        catch (System.Exception)
        {
            string path=PathMgr.logDirectory;
            int maxFileNum=PathMgr.logMaxFile;

            //文件夹目录
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //文件个数限制
            string[] logFiles=Directory.GetFiles(path);
            if (logFiles.Length>maxFileNum)
            {
                File.Delete(logFiles[0]);
            }
            
            Write(logFileName,log,true);
        }
    }

    /// <summary>
    /// 文件写入
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="content"></param>
    /// <param name="isAppend">是否保留原内容</param>
    private static void Write(string filePath,string content,bool isAppend)
    {
        using (StreamWriter sw = new StreamWriter(filePath, isAppend, Encoding.UTF8))
        {
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
