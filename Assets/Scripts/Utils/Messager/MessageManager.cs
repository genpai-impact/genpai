using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                CreateAreaManager(area).Subscribe();
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
            GetManager(areaCode).Execute(eventCode, message);
        }


    }

    /// <summary>
    /// 域消息管理器，用于发布域内消息
    /// </summary>
    public class AreaMessageManager : IMessageReceiveHandler
    {
        /// <summary>
        /// 管辖消息域
        /// </summary>
        protected MessageArea areaCode;

        /// <summary>
        /// 订阅名单：<消息号,监听列表>
        /// </summary>
        private Dictionary<string, HashSet<IMessageReceiveHandler>> ListenerDict;


        public AreaMessageManager(MessageArea _areaCode)
        {
            this.areaCode = _areaCode;
        }

        /// <summary>
        /// 添加消息接收端
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="listener">接收者</param>
        public void AddListener(string eventCode, IMessageReceiveHandler listener)
        {
            if (ListenerDict.ContainsKey(eventCode))
            {
                ListenerDict[eventCode].Add(listener);
            }
            else
            {
                ListenerDict[eventCode] = new HashSet<IMessageReceiveHandler>();
                ListenerDict[eventCode].Add(listener);
            }
        }

        /// <summary>
        /// 删除消息接收端（收件方析构时调用）
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="listener">接收者</param>
        public void RemoveListener(string eventCode, IMessageReceiveHandler listener)
        {
            ListenerDict[eventCode].Remove(listener);
        }

        /// <summary>
        /// 向自己管理的终端分发消息
        /// </summary>
        /// <param name="eventCode">事件码</param>
        /// <param name="message">消息</param>
        public void Execute(string eventCode, object message)
        {
            if (ListenerDict.ContainsKey(eventCode))
            {
                foreach (var listener in ListenerDict[eventCode])
                {
                    listener.Execute(eventCode, message);
                }
            }
        }

        /// <summary>
        /// 向消息中心订阅
        /// </summary>
        public void Subscribe()
        {
            MessageManager.Instance.AddAreaManager(this.areaCode, this);
        }


    }

}

