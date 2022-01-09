using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Buff基类
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d966026452a8003fd609a5
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
