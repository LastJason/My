using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBase : MonoBehaviour
{
    #region Private members

    /// <summary>
    /// 对象池id
    /// </summary>
    private string _name;

    /// <summary>
    /// 对象池trans
    /// </summary>
    private Transform _trans;

    /// <summary>
    /// 对象池预制体
    /// </summary>
    private GameObject _prefab;

    /// <summary>
    /// 预制体列表
    /// </summary>
    private List<Transform> _list;

    /// <summary>
    /// 预制体初始数
    /// </summary>
    private int _numinit = 1;

    /// <summary>
    /// 预制体最大数
    /// </summary>
    private int _nummax = 50;

    /// <summary>
    /// 销毁预制体单次最大个数
    /// </summary>
    private int _numcleargap = 9999;

    /// <summary>
    /// 正在销毁预制体
    /// </summary>
    private bool _isInDs = false;

    #endregion


    #region public methods

    /// <summary>
    /// 缓存池初始化
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    /// <param name="prefab"></param>
    /// <param name="initnum"></param>
    public void Init(string name, Transform parent, GameObject prefab, int initnum)
    {
        this.transform.name = name;
        this.transform.SetParent(parent);

        _name = name;
        _trans = this.transform;

        _list = new List<Transform>();
        _prefab = prefab;
        _numinit = initnum;

        _Preload();
    }

    /// <summary>
    /// 取物体
    /// </summary>
    /// <returns></returns>
    public Transform Get(Transform parent, Vector3 pos, Quaternion rot, Vector3 scale)
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
            trans.name = _prefab.name;
        }

        trans.SetParent(parent);
        trans.SetPositionAndRotation(pos, rot);
        trans.localScale = scale;
        trans.gameObject.SetActive(true);
        return trans;
    }

    public Transform Get(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        return Get(null, pos, rot, scale);
    }

    public Transform Get(Vector3 pos)
    {
        return Get(null, pos, Quaternion.identity, Vector3.one);
    }

    public Transform Get()
    {
        return Get(null, Vector3.zero, Quaternion.identity, Vector3.one);
    }

    /// <summary>
    /// 放物体
    /// </summary>
    /// <param name="obj"></param>
    public void Put(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = _trans;
        _list.Add(obj.transform);

        _DestroyOnTime();
    }

    /// <summary>
    /// 清空对象池
    /// </summary>
    public void Clear()
    {
        if (_isInDs)
        {
            StopCoroutine("_CoDestroy");
        }
        _list.Clear();
        Destroy(_trans.gameObject);
    }

    /// <summary>
    /// 缓存池Name
    /// </summary>
    public string Name
    {
        get { return _name; }
    }

    /// <summary>
    /// 缓存池Trans
    /// </summary>
    public Transform Trans
    {
        get { return _trans; }
    }

    /// <summary>
    /// 单次销毁预制体最大个数
    /// </summary>
    public int ClearPerNum
    {
        get { return _numcleargap; }
        set { _numcleargap = value; }
    }

    #endregion

    #region private methods

    /// <summary>
    /// 预加载预制体
    /// </summary>
    private void _Preload()
    {
        Transform prefabIns;
        for (int i = 0; i < _numinit; i++)
        {
            prefabIns = Instantiate(_prefab).transform;
            prefabIns.gameObject.SetActive(false);
            prefabIns.name = _prefab.name;
            prefabIns.parent = _trans;
            _list.Add(prefabIns);
        }
    }

    /// <summary>
    /// 检测预制体过多
    /// </summary>
    private void _DestroyOnTime()
    {
        if (_isInDs) return;
        if (_list.Count > _nummax)
        {
            _isInDs = true;
            StartCoroutine("_CoDestroy");
        }
    }

    /// <summary>
    /// 删除多余预制体
    /// </summary>
    /// <returns></returns>
    private IEnumerator _CoDestroy()
    {
        while (true)
        {
            int numoffset = _list.Count - _nummax;
            if (numoffset >= _numcleargap)
            {
                numoffset = _numcleargap;
            }

            for (int i = 0; i < numoffset; i++)
            {
                Destroy(_list[0].gameObject);
                _list.Remove(_list[0]);
            }
            if (_list.Count <= _nummax)
            {
                break;
            }
        }

        Debug.Log("PoolBase:_CoDestroy");
        _isInDs = false;
        yield return null;
    }

    #endregion
}
