using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    private static Pool<T> current;
    private Dictionary<string, Stack<T>> pool = new Dictionary<string, Stack<T>>();

    private void Awake()
    {
        current = this;
    }

    public static T Get(string name, T defaultItem)
    {
        T i = current._Get(name, defaultItem);
        i.transform.parent = current.transform;
        return i;
    }

    private T _Get(string name, T defaultItem)
    {
        if (pool.ContainsKey(name))
        {
            if (pool[name].Count > 0)
            {
                return pool[name].Pop();
            }
        }
        else
        {
            pool.Add(name, new Stack<T>());
        }
        return Instantiate(defaultItem.gameObject, Vector3.zero, Quaternion.identity).GetComponent<T>();
    }

    public static void Release(string name, T t)
    {
        t.gameObject.SetActive(false);

        current._Release(name, t);
    }

    private void _Release(string name, T t)
    {
        if (pool.ContainsKey(name))
        {
            pool[name].Push(t);
        }
        else
        {
            pool.Add(name, new Stack<T>());
            pool[name].Push(t);
        }
    }

    public static int Count()
    {
        int i = 0;
        foreach (var item in current.pool)
        {
            i += item.Value.Count;
        }

        return i;
    }
}
