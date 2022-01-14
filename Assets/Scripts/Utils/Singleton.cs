using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例泛型
/// </summary>
/// <typeparam name="T">单例类型</typeparam>
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
