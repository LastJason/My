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
            sw.WriteLine(Tools.GetDataTime() + ": " + content);
            sw.Flush();
            sw.Close();
        }
    }

    /// <summary>
    /// 获取PanelData
    /// </summary>
    /// <returns></returns>
    public Dictionary<PanelType,string> GetPanelInfoData()
    {
        string filepath = StaticConst.PanelInfoPath;
        if (!File.Exists(filepath))
        {
            return null;
        }

        Dictionary<PanelType, string> panelDic = new Dictionary<PanelType, string>();
        PanelInfoJson data = JsonUtility.FromJson<PanelInfoJson>(File.ReadAllText(filepath));

        foreach (var item in data.infoList)
        {
            panelDic.Add(item.Type, item.Path);
        }
        return panelDic;
    }
}
