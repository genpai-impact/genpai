using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using Messager;


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
        //MoveToLast,
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
        //private int MaxCardNum=10;
        //public GameObject newCard;
        public GenpaiPlayer waitingPlayer;


        /// <summary>
        /// 执行订阅操作
        /// </summary>
        public void Subscribe<T>(HandCardMassage handCardMassage, UnityAction<T> action)
        {
            // 向模块管理器追加订阅
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
        /// 执行消息分发操作（执行监听事件）
        /// </summary>
        public void SendMessage<T>(HandCardMassage handCardMassage, T data)
        {
            //Debug.Log("sendmessage "+ dictionaryMessage.Count+data.ToString());

            if (dictionaryMessage.TryGetValue(handCardMassage, out var previousAction))
            {
                //Debug.Log("............................execute");
                (previousAction as MessageData<T>)?.MessageEvents.Invoke(data);
            }



        }

    }
}