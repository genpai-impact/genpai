using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Buff基类
    /// </summary>
    public abstract class BaseBuff : IMessageReceiveHandler
    {
        /// <summary>
        /// 消息接受接口，订阅并接收回合消息
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
        public void Execute(int eventCode, object message) { }

        public void Subscribe() { }
    }
}
