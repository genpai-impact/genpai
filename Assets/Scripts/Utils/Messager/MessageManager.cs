using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Messager
{
    /// <summary>
    /// 消息中心，用于接收并发布所有消息
    /// </summary>
    public class MessageManager : Singleton<MessageManager>, IMessageSendHandler
    {
        private Dictionary<MessageArea, AreaMessageManager> managers = new Dictionary<MessageArea, AreaMessageManager>();

        private void Awake()
        {
            // 自动循环域列表创建管理器
            foreach (MessageArea area in System.Enum.GetValues(typeof(MessageArea)))
            {
                CreateAreaManager(area);
            }

        }

        /// <summary>
        /// 添加域管理器（由域管理器订阅时调用）
        /// </summary>
        /// <param name="areaCode">域名</param>
        /// <param name="areaManager">管理器</param>
        public void AddAreaManager(MessageArea areaCode, AreaMessageManager areaManager)
        {
            managers.Add(areaCode, areaManager);

        }

        /// <summary>
        /// 创建域管理器（在启动时调用）
        /// </summary>
        /// <param name="areaCode">域名</param>
        /// <returns>域管理器</returns>
        public AreaMessageManager CreateAreaManager(MessageArea areaCode)
        {
            return new AreaMessageManager(areaCode);
        }

        /// <summary>
        /// 获取对应域管理器
        /// </summary>
        /// <param name="areaCode">域名</param>
        /// <returns>域管理器</returns>
        public AreaMessageManager GetManager(MessageArea areaCode)
        {
            if (managers.ContainsKey(areaCode))
            {
                return managers[areaCode];
            }
            else
            {
                // Debug.Log("不存在管理器：" + areaCode.ToString());
                return null;
            }

        }

        /// <summary>
        /// 将信息分发予域管理器
        /// </summary>
        /// <param name="areaCode">域管理器编号</param>
        /// <param name="eventCode">域内事件编号</param>
        /// <param name="message">消息</param>
        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            // GetManager(areaCode).Execute(eventCode, message);
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
        public MessageArea areaCode;

        /// <summary>
        /// 订阅名单：<消息,监听列表>
        /// </summary>
        private Dictionary<string, IMessageData> dictionaryMessage = new Dictionary<string, IMessageData>();


        public AreaMessageManager(MessageArea _areaCode)
        {
            this.areaCode = _areaCode;
            MessageManager.Instance.AddAreaManager(this.areaCode, this);
        }

        /// <summary>
        /// 向管理器订阅
        /// 由目标体调用
        /// </summary>
        /// <typeparam name="T">消息数据包泛型</typeparam>
        /// <param name="Massage">消息类型</param>
        /// <param name="action">消息反应函数</param>
        public void Subscribe<T>(string Massage, UnityAction<T> action)
        {

            if (dictionaryMessage.TryGetValue(Massage, out var previousAction))
            {
                if (previousAction is MessageData<T> messageData)
                {
                    messageData.MessageEvents += action;
                }
            }
            else
            {
                dictionaryMessage.Add(Massage, new MessageData<T>(action));
            }

        }


        /// <summary>
        /// 执行消息分发
        /// 由管理器调用
        /// </summary>
        /// <typeparam name="T">分发消息类型</typeparam>
        /// <param name="Massage">待分发消息</param>
        /// <param name="data">消息</param>
        public void ExecuteMessage<T>(string Massage, T data)
        {

            if (dictionaryMessage.TryGetValue(Massage, out var previousAction))
            {

                (previousAction as MessageData<T>)?.MessageEvents.Invoke(data);
            }

        }

        /// <summary>
        /// 删除消息接收端（收件方析构时调用）
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="listener">接收者</param>
        public void RemoveListener<T>(string Massage, UnityAction<T> action)
        {

        }

    }

}

