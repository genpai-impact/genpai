using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Buff����
    /// </summary>
    public abstract class BaseBuff : IMessageReceiveHandler
    {
        /// <summary>
        /// ��Ϣ���ܽӿڣ����Ĳ����ջغ���Ϣ
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
        public void Execute(int eventCode, object message) { }

        public void Subscribe() { }
    }
}
