using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public PanelType Type;

    [SerializeField]
    public string Typestr;

    [SerializeField]
    public string Path;

    public PanelInfo(PanelType type, string path)
    {
        this.Type = type;
        this.Path = path;
    }

    public void OnAfterDeserialize()
    {
        this.Type = (PanelType)Enum.Parse(typeof(PanelType), Typestr);
    }

    public void OnBeforeSerialize()
    {

    }
}

public enum PanelType
{
    MainMenu,
    Knapsack,
    Shop,
    Skill,
    Task,
    System,
    ItemMessage
}

/// <summary>
/// 辅助类 解析json数据
/// </summary>
[Serializable]
public class PanelInfoJson {

    public List<PanelInfo> infoList;

}
