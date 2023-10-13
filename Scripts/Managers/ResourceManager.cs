using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public GameObject Instantiate(GameObject original, Transform parent = null)
    {
        if (original.GetComponent<Poolable>() != null)
            return PoolManager.Instance.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public T Instantiate<T>(string name, Transform parent = null) where T : MonoBehaviour
    {
        var go = Instantiate(name, parent);
        var component = go.GetComponent<T>();
        return component;
    }

    public T Instantiate<T>(GameObject original, Transform parent = null) where T : MonoBehaviour
    {
        var go = Instantiate(original, parent);
        var component = go.GetComponent<T>();
        return component;
    }

    public GameObject Instantiate(string name, Transform parent = null)
    {
        var obj = PatchManager.Instance.Get(name);

        if (obj.GetComponent<Poolable>() != null)
            return PoolManager.Instance.Pop(obj, parent).gameObject;

        GameObject go = Object.Instantiate(obj, parent);
        go.name = obj.name;
        return go;
    }

    public void Destroy(GameObject go, float time = 0)
    {
        if (go == null)
        {
            return;
        }

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            PoolManager.Instance.Push(poolable);
            return;
        }

        Object.Destroy(go, time);
    }
}
