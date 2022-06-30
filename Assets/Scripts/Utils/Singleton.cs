using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 单例泛型(组件)
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
        }
    
        public void Clean()
        {
            _instance = null;
        }
    }

    /// <summary>
    /// 单例泛型（标准）
    /// </summary>
    public class Singleton<T> where T : class
    {
        private static readonly object InitLock = new object();

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }
                return _instance;
            }
        }

        private static void CreateInstance()
        {
            lock (InitLock)
            {
                if (_instance != null) return;
                var t = typeof(T);

                _instance = (T) Activator.CreateInstance(t, true);
            }
        }
    
        public void Clean()
        {
            _instance = null;
        }



    }
}