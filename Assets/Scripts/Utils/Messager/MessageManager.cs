using System.Collections.Generic;
using UnityEngine.Events;

namespace Utils.Messager
{
    /// <summary>
    /// 消息中心，用于接收并发布所有消息
    /// </summary>
    public class MessageManager : Singleton<MessageManager>
    {
        public Dictionary<MessageArea, AreaMessageManager> managers = new Dictionary<MessageArea, AreaMessageManager>();

        private MessageManager()
        {
            foreach (MessageArea area in System.Enum.GetValues(typeof(MessageArea)))
            {
                if (!managers.ContainsKey(area))
                {
                    managers.Add(area, new AreaMessageManager(area));
                }

            }
        }


        /// <summary>
        /// 获取对应域管理器
        /// 主要用于主动订阅过程
        /// </summary>
        /// <param name="areaCode">域名</param>
        /// <returns>域管理器</returns>
        public AreaMessageManager GetManager(MessageArea areaCode)
        {
            if (managers.ContainsKey(areaCode))  // 如果域名areaCode存在在managers中
            {
                return managers[areaCode];  // 返回areaCode对应的AreaMessageManager
            }
            else  // 如果域名areaCode不存在在managers中，往managers中添加新的,并返回此AreaMessageManager
            {
                managers.Add(areaCode, new AreaMessageManager(areaCode));
                return managers[areaCode];
            }
            // Debug.Log("不存在管理器：" + areaCode.ToString());

        }

        /// <summary>
        /// 将信息分发予域管理器
        /// </summary>
        /// <param name="areaCode">域管理器编号</param>
        /// <param name="eventCode">域内事件编号</param>
        /// <param name="messageData">消息</param>
        public void Dispatch<T>(MessageArea areaCode, string eventCode, T messageData)
        {
            GetManager(areaCode).ExecuteMessage<T>(eventCode, messageData);
        }


    }

    /// <summary>
    /// 域消息管理器，管理特定域内消息
    /// </summary>
    public class AreaMessageManager
    {
        /// <summary>
        /// 制定消息域
        /// </summary>
        public readonly MessageArea areaCode;

        /// <summary>
        /// 订阅名单（消息, 监听方法列表）
        /// </summary>
        private Dictionary<string, IMessage> dictionaryMessage = new Dictionary<string, IMessage>();


        public AreaMessageManager(MessageArea _areaCode)
        {
            this.areaCode = _areaCode;
        }

        /// <summary>
        /// 向管理器订阅
        /// 由目标体调用
        /// </summary>
        /// <typeparam name="T">消息数据包类型</typeparam>
        /// <param name="MessageCode">消息码</param>
        /// <param name="action">对象在接收消息是实现的方法</param>
        public void Subscribe<T>(string MessageCode, UnityAction<T> action)
        {
            // Debug.Log(areaCode + " has Subscribe");
            if (dictionaryMessage.TryGetValue(MessageCode, out var previousAction))
            {
                if (previousAction is Message<T> messageData)
                {
                    messageData.MessageEvents += action;
                  
                }
              //  if (previousAction is Message<List<bool>> mm) Debug.LogError(mm.MessageEvents);
            }
            else
            {
                dictionaryMessage.Add(MessageCode, new Message<T>(action));  // 添加新的订阅
            }

        }


        /// <summary>
        /// 执行消息分发
        /// 由管理器调用
        /// </summary>
        /// <typeparam name="T">分发消息类型</typeparam>
        /// <param name="MessageCode">待分发消息</param>
        /// <param name="data">消息</param>
        public void ExecuteMessage<T>(string MessageCode, T data)
        {

            //Debug.LogWarning("ExecuteMessage");
            if (dictionaryMessage.TryGetValue(MessageCode, out var previousAction))
            {
                //Debug.LogWarning("已找到注册函数：" + MessageCode);
                //(previousAction as Message<T>) ?
                //Debug.LogWarning((previousAction as Message<T>)?.MessageEvents);
                (previousAction as Message<T>)?.MessageEvents.Invoke(data);
            }

        }

        /// <summary>
        /// 删除消息监听
        /// </summary>
        /// <typeparam name="T">消息数据包类型</typeparam>
        /// <param name="MessageCode">消息码</param>
        /// <param name="action">对象在接收消息是实现的方法</param>
        public void RemoveListener<T>(string MessageCode, UnityAction<T> action)
        {
            if (dictionaryMessage.TryGetValue(MessageCode, out var previousAction))
            {
                if (previousAction is Message<T> messageData)
                {
                    // 能这样取消监听吗？
                    messageData.MessageEvents -= action;
                }
            }
        }

    }

}

