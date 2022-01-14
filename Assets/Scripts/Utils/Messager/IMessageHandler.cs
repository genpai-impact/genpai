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
        public void Dispatch(MessageArea areaCode, int eventCode, object message);
    }

    /// <summary>
    /// 消息接收接口
    /// </summary>
    public interface IMessageReceiveHandler
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="eventCode">事件</param>
        /// <param name="message">消息</param>
        public void Execute(int eventCode, object message);

        /// <summary>
        /// 订阅消息
        /// （向域管理器确定自身所在域）
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

