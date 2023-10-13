using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Pool
{
    public GameObject original { get; private set; }
    public Transform Root { get; set; }

    Stack<Poolable> poolStack = new Stack<Poolable>();

    public void Init(GameObject originalObj, int count = 1)
    {
        original = originalObj;
        Root = new GameObject().transform;
        Root.name = $"{original.name}_Root";
        Root.transform.parent = PoolManager.Instance._root;
        
        for (int i = 0; i < count; i++)
            Push(Create());
    }
    
    private Poolable Create()
    {
        GameObject go = Object.Instantiate(original);
        go.name = original.name;
        return go.GetComponent<Poolable>();
    }

    public void Push(Poolable poolable)
    {
        if (poolable == null) return;

        poolable.transform.SetParent(Root);
        poolable.gameObject.SetActive(false);
        poolable.isUsing = false;

        poolStack.Push(poolable);
    }

    public Poolable Pop(Transform parent)
    {
        Poolable poolable;

        if (poolStack.Count > 0) 
            poolable = poolStack.Pop();
        else 
            poolable = Create();

        poolable.gameObject.SetActive(true);

        poolable.transform.SetParent(parent);
        poolable.isUsing = true;

        return poolable;
    }
}
