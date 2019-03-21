using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    private CanvasGroup _canvas;

    protected virtual void Start()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    public virtual void OnEnter()
    {
        Debug.LogError("enter-" + this.name);
        this.gameObject.SetActive(true);
    }

    public virtual void OnPase()
    {
        _canvas.blocksRaycasts = false;
        Debug.LogError("pause-" + this.name);
        _canvas.alpha = 0.5f;
    }

    //刷新一下数据
    public virtual void OnResume()
    {
        _canvas.blocksRaycasts = true;
        Debug.LogError("resume-" + this.name);
        _canvas.alpha = 1;
    }

    public virtual void OnExit()
    {
        Debug.LogError("exit-" + this.name);
        this.gameObject.SetActive(false);
    }
}
