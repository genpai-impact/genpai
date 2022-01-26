using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Messager;


namespace Genpai
{
    /// <summary>
    /// 卡牌管理器的消息类型
    /// None：无消息
    /// MoveTo：向目标移动
    /// </summary>
    public enum HandCardMassage
    {
        None,
        MoveTo
    }

    /// <summary>
    /// MoveTo消息类型的数据包
    /// gameObject：指明哪个物体
    /// target：目标位置
    /// </summary>
    public class MoveToData
    {
        public GameObject gameObject;
        public Vector3 target;
        public MoveToData(GameObject obj, Vector3 vec)
        {
            gameObject = obj;
            target = vec;
        }
    }

    /// <summary>
    /// 卡牌管理器，用于实现卡（手）牌消息的接受和分发
    /// </summary>
    public class HandCardManager : MonoSingleton<HandCardManager> //IMessageHandler
    {
        /// <summary>
        /// 订阅名单：<消息类型,监听列表>
        /// </summary>
        private Dictionary<HandCardMassage, IMessageData> dictionaryMessage = new Dictionary<HandCardMassage, IMessageData>();

        public List<GameObject> handCards;

        public GenpaiPlayer waitingPlayer;


        /// <summary>
        /// 向管理器订阅
        /// 由目标体调用
        /// </summary>
        /// <typeparam name="T">消息数据包泛型</typeparam>
        /// <param name="handCardMassage">消息类型</param>
        /// <param name="action">消息反应函数</param>
        public void Subscribe<T>(HandCardMassage handCardMassage, UnityAction<T> action)
        {

            if (dictionaryMessage.TryGetValue(handCardMassage, out var previousAction))
            {
                if (previousAction is MessageData<T> messageData)
                {
                    messageData.MessageEvents += action;
                }
            }
            else
            {
                dictionaryMessage.Add(handCardMassage, new MessageData<T>(action));
            }

        }



        /// <summary>
        /// 执行消息分发
        /// 由管理器调用
        /// </summary>
        /// <typeparam name="T">分发消息类型</typeparam>
        /// <param name="handCardMassage">待分发消息</param>
        /// <param name="data">消息</param>
        public void ExecuteMessage<T>(HandCardMassage handCardMassage, T data)
        {

            if (dictionaryMessage.TryGetValue(handCardMassage, out var previousAction))
            {

                (previousAction as MessageData<T>)?.MessageEvents.Invoke(data);
            }



        }

    }
}