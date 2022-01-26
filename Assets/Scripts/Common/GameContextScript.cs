﻿
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 上下文脚本
    /// </summary>
    public class GameContextScript : MonoBehaviour
    {
        public void Start()
        {
            Debug.Log("game context is " + GameContext.Instance);
            GameContext.Instance.Init();
            Messager.MessageManager.Instance.Init();
            NormalProcessManager.Instance.Start();
        }

        public void Update()
        {
            // Debug.Log("current process is " + NormalProcessManager.Instance.GetCurrentProcess().GetName());
        }
    }
}
