using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

public class FileMgr : Singleton<FileMgr>
{
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
}
