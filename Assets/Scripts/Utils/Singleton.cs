using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

/// <summary>
/// 单例泛型(组件)
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}

/// <summary>
/// 单例泛型（标准）
/// </summary>
public class Singleton<T> where T : class
{
    private static object initLock = new object();

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                CreateInstance();
            }
            return instance;
        }
    }

    private static void CreateInstance()
    {
        lock (initLock)
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)Activator.CreateInstance(t, true);
            }
        }
    }

}
