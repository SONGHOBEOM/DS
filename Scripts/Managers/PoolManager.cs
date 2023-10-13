using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private Transform scenePos;
    private Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    public Transform _root;

    protected override void Awake()
    {
        Init();
    }
    public void Init()
    {
        if(_root == null)
        {
            GameObject poolRoot = new GameObject { name = "@Pool_Root" };
            _root = poolRoot.transform;
            Object.DontDestroyOnLoad(poolRoot);
        }
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public void CreatePool(GameObject original, int count = 1)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }
}
