using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Messager
{
    /// <summary>
    /// 消息发送接口
    /// </summary>
    public interface IMessageSendHandler
    {
        /// <summary>
        /// 实现：MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        /// </summary>
        public void Dispatch(MessageArea areaCode, string eventCode, object message);
    }

    /// <summary>
    /// 消息接收接口
    /// </summary>
    public interface IMessageReceiveHandler
    {

        /// <summary>
        /// 订阅消息
        /// 实现：Messager.MessageManager.Instance.GetManager(area).Subscribe<T>(messageEvent, Func<T>);
        /// </summary>
        public void Subscribe();
    }

    /// <summary>
    /// 通用消息接口
    /// </summary>
    public interface IMessageHandler : IMessageSendHandler, IMessageReceiveHandler
    {

    }

}

