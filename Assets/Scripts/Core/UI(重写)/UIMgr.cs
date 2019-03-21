using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : Singleton<UIMgr>
{
    protected override void Init()
    {
        GameObject obj = new GameObject();
        obj.name = "----UI----";
        _trans = obj.transform;

        _panelPathDic = FileMgr.Instance.GetPanelInfoData();
        _panelDic = new Dictionary<PanelType, PanelBase>();
        _panelStack = new Stack<PanelBase>();
    }

    private Dictionary<PanelType, string> _panelPathDic;

    private Dictionary<PanelType, PanelBase> _panelDic;

    private Transform _trans;

    private Stack<PanelBase> _panelStack;

    // 创建
    public void ShowPanel(PanelType type)
    {
        PanelBase panel = CreateInst(type);
        if (_panelStack.Contains(panel))
        {
            if (_panelStack.Count==0)
            {
                _panelStack.Peek().OnResume();
            }
            else
            {
                _panelStack.Peek().OnPase();
                panel.OnResume();
                _panelStack.Push(panel);

            }
        }
        else
        {
            if (_panelStack.Count==0)
            {
                _panelStack.Push(panel);
            }
            else
            {
                _panelStack.Peek().OnPase();
                _panelStack.Push(panel);
            }
        }
    }

    public void ClosePanel()
    {
        if (_panelStack.Count == 0) return;

        _panelStack.Pop().OnExit();

        if (_panelStack.Count > 0)
        {
            _panelStack.Peek().OnResume();
        }
    }

    private PanelBase CreateInst(PanelType type)
    {
        if (_panelDic.ContainsKey(type))
        {
            _panelDic[type].OnEnter();
            return _panelDic[type];
        }

        GameObject obj = GameObject.Instantiate(Resources.Load(_panelPathDic[type])) as GameObject;
        obj.transform.parent = _trans;

        PanelBase panel = obj.GetComponent<PanelBase>();
        panel.OnEnter();
        _panelDic.Add(type, panel);
        return panel;
    }

    
}
