using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
    /// <summary>
    /// 对象池id
    /// </summary>
    protected string _name;

    /// <summary>
    /// 对象池trans
    /// </summary>
    protected Transform _trans;

    /// <summary>
    /// 对象池预制体
    /// </summary>
    protected GameObject _prefab;

    /// <summary>
    /// 预制体列表
    /// </summary>
    protected List<Transform> _list;

    /// <summary>
    /// 预制体初始数
    /// </summary>
    protected int _numinit;

    /// <summary>
    /// 预制体最大数
    /// </summary>
    protected int _nummax = 4;

    /// <summary>
    /// 销毁预制体时间间隔
    /// </summary>
    protected float _timecleargap = 1;

    /// <summary>
    /// 销毁预制体每次个数
    /// </summary>
    protected int _numcleargap=3;

    /// <summary>
    /// 待拓展 拓展构造函数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <param name="prefab"></param>
    /// <param name="maxnum"></param>
    public PoolBase(string name, Transform parent, GameObject prefab, int maxnum)
    {
        Init(name, parent, prefab, maxnum);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <param name="prefab"></param>
    /// <param name="maxnum"></param>
    private void Init(string name, Transform parent, GameObject prefab, int maxnum)
    {
        //创建
        _name = name;
        _prefab = prefab;
        _nummax = maxnum;
        _list = new List<Transform>();

        GameObject obj = new GameObject();
        _trans = obj.transform;
        _trans.SetParent(parent);
    }


    /// <summary>
    /// 取物体
    /// </summary>
    /// <returns></returns>
    public virtual Transform Get(Transform parent, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Transform trans;
        if (_list.Count > 0)
        {
            trans = _list[0];
            _list.Remove(trans);
        }
        else
        {
            trans = GameObject.Instantiate(_prefab).transform;
        }

        trans.SetParent(parent);
        trans.SetPositionAndRotation(pos, rot);
        trans.localScale = scale;
        trans.gameObject.SetActive(true);
        return trans;
    }

    /// <summary>
    /// 放物体
    /// </summary>
    /// <param name="obj"></param>
    public virtual void Put(GameObject obj)
    {
        obj.SetActive(false);
        _list.Add(obj.transform);

        _DestroyOnTime();
    }

    /// <summary>
    /// 清空对象池
    /// </summary>
    public virtual void Clear()
    {
        _list.Clear();
        for (int i = 0; i < _trans.childCount; i++)
        {
            Destroy(_trans.GetChild(i));
        }
    }

    /// <summary>
    /// 检测销毁多余预制体
    /// </summary>
    private void _DestroyOnTime()
    {
        //_nummax = 4;
        //_numcleargap = 3;
        //_timecleargap = 2;

        if (_isInDs) return;
        if (_list.Count > _nummax)
        {
            InvokeRepeating("_CoDestroy", 1, _timecleargap);
            //StartCoroutine(_CoDestroy());
        }
    }

    private bool _isInDs = false;

    /// <summary>
    /// invokerepeat实现
    /// </summary>
    private void _CoDestroy()
    {
        _isInDs = true;

        int numoffset = _list.Count - _nummax;
        bool isbreak = false;
        if (numoffset > _numcleargap)
        {
            numoffset = _numcleargap;
        }
        else
            isbreak = true;
        for (int i = 0; i < numoffset; i++)
        {
            Destroy(_list[0].gameObject);
            _list.Remove(_list[0]);
        }

        if (isbreak) {
            _isInDs = false;
            CancelInvoke("_CoDestroy");
        }
    }

    /// <summary>
    /// 协程实现
    /// </summary>
    /// <returns></returns>
    //private IEnumerator _CoDestroy()
    //{
    //    _isInDs = true;
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(_timecleargap);
    //        //删除
    //        int numoffset = _list.Count - _nummax;
    //        bool isbreak = false;
    //        if (numoffset > _numcleargap)
    //        {
    //            numoffset = _numcleargap;
    //        }
    //        else
    //            isbreak = true;
    //        for (int i = 0; i < numoffset; i++)
    //        {
    //            Destroy(_list[0].gameObject);
    //            _list.Remove(_list[0]);
    //        }

    //        if (isbreak) break;
    //    }
    //    Debug.LogError("!!!!!!!!!!!!!!");
    //    _isInDs = false;
    //}

    private void Start()
    {
        //_list = new List<Transform>();
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_list.Add(Camera.main.transform);
        //_DestroyOnTime();

    }
}
