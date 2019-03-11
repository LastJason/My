using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : SingletonMono<ShowFPS>
{
    //显示
    private float aveFps;
    private string txtFps;

    //计时
    private float _timegap = 1;
    private float _timecur;

    //计总量
    private int _numframes;
    private float _timeframes;

    //开始
    private bool _isstart = false;
    public bool Isstart
    {
        get { return _isstart; }
        set
        {
            _isstart = value;
            if (_isstart)
            {
                _timecur = _timegap;
                _timeframes = 0;
                _numframes = 0;
                txtFps = string.Format("FPS:0");
            }
        }
    }

    private void Update()
    {
        if (!_isstart)
        {
            return;
        }
        _timecur -= Time.deltaTime;
        _timeframes += Time.timeScale / Time.deltaTime;
        ++_numframes;
        if (_timecur <= 0)
        {
            aveFps = _timeframes / _numframes;
            txtFps = string.Format("FPS:" + aveFps);
            _timecur = _timegap;
            _timeframes = 0;
            _numframes = 0;
        }
    }

    private void OnGUI()
    {
        if (!Isstart)
        {
            return;
        }
        GUI.Label(new Rect(0, 0, 200, 100), txtFps);
    }
}