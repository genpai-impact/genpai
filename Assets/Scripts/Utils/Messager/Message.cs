using UnityEngine.Events;

namespace Utils.Messager
{

    /// <summary>
    /// 消息数据接口
    /// 继承该接口视为消息数据包
    /// （只是个没用的标识）
    /// </summary>
    public interface IMessageData { };

    /// <summary>
    /// 消息接口
    /// 继承该接口则视为消息
    /// （用于保护对应不同消息类型的消息）
    /// </summary>
    public interface IMessage { };

    /// <summary>
    /// 消息类
    /// </summary>
    /// <typeparam name="T">消息数据类型</typeparam>
    public class Message<T> : IMessage
    {
        public UnityAction<T> MessageEvents;
        public Message(UnityAction<T> action)
        {
            MessageEvents += action;
        }
    }
}
