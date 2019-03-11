using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : Singleton<PoolMgr>
{
    /// <summary>
    /// 对象池父物体trans
    /// </summary>
    private Transform _trans;

    /// <summary>
    /// 对象池列表
    /// </summary>
    private Dictionary<string,PoolBase> _pooldic;

    protected override void Init()
    {
        GameObject poolPar = new GameObject();
        poolPar.name = "[POOL]";

        _trans = poolPar.transform;
        _pooldic = new Dictionary<string, PoolBase>();
    }

    public PoolBase Create(string name,GameObject prefab,int initnum)
    {
        if (_pooldic.ContainsKey(name))
        {
            Debug.LogError("[POOL]:" + name + " is exist");
            return _pooldic[name];
        }

        GameObject obj = new GameObject();
        PoolBase pool = obj.AddComponent<PoolBase>();
        pool.Init(name, _trans, prefab, initnum);
        _pooldic.Add(name,pool);
        return pool;
    }

    public PoolBase Create(string name, GameObject prefab)
    {
        return Create(name, prefab, 1);
    }

    public void Remove(string name)
    {
        if (!_pooldic.ContainsKey(name))
        {
            Debug.LogError("[POOL]:" + name + " is not exist");
            return;
        }
        
        PoolBase pool = _pooldic[name];
        pool.Clear();
        _pooldic.Remove(name);
    }

    public void Remove(PoolBase pool)
    {
        Remove(pool.Name);
    }
    
    public void Reset()
    {
        foreach (KeyValuePair<string,PoolBase> pool in _pooldic)
        {
            pool.Value.Clear();
        }
        _pooldic.Clear();
    }
}
