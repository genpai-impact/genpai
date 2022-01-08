using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Messager
{
    public interface IMessageSendHandler
    {
        /// <summary>
        /// 实现：MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        /// </summary>
        public void Dispatch(MessageArea areaCode, int eventCode, object message);
    }

    public interface IMessageReceiveHandler
    {
        // 处理消息
        public void Execute(int eventCode, object message);

        // 向消息中心订阅消息（确定自身所在域）
        public void Subscribe();
    }
    public interface IMessageHandler : IMessageSendHandler, IMessageReceiveHandler
    {

    }

}

