using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Genpai
{
    /// <summary>
    /// 不同参数类型消息接口封装
    /// </summary>
    public interface IMessageData
    {

    }

    /// <summary>
    /// 泛型监听事件（事件的参数为 T <泛型>）
    /// </summary>
    public class MessageData<T> : IMessageData
    {
        public UnityAction<T> MessageEvents;
        public MessageData(UnityAction<T> action)
        {
            MessageEvents += action;
        }
    }
}
