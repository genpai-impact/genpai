using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class SpellPlayerController : MonoBehaviour, IMessageSendHandler
    {
        public SpellCard spellCard;
        public BattleSite playerSite;
        private void Awake()
        {

            InitTrigger();
        }
        /// <summary>
        /// 初始化鼠标事件触发器
        /// </summary>
        public void InitTrigger()
        {

            UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyOnMouseDown);
            EventTrigger.Entry myClick = new EventTrigger.Entry();
            myClick.eventID = EventTriggerType.PointerDown;
            myClick.callback.AddListener(click);

            UnityAction<BaseEventData> enter = new UnityAction<BaseEventData>(MyOnMouseEnter);
            EventTrigger.Entry myEnter = new EventTrigger.Entry();
            myEnter.eventID = EventTriggerType.PointerEnter;
            myEnter.callback.AddListener(enter);

            UnityAction<BaseEventData> exit = new UnityAction<BaseEventData>(MyOnMouseExit);
            EventTrigger.Entry myExit = new EventTrigger.Entry();
            myExit.eventID = EventTriggerType.PointerExit;
            myExit.callback.AddListener(exit);


            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            trigger.triggers.Add(myClick);
            trigger.triggers.Add(myEnter);
            trigger.triggers.Add(myExit);
        }
        void MyOnMouseEnter(BaseEventData data)
        {
            Debug.Log("PointerEnter");
            spellCard = GetComponent<CardDisplay>().card as SpellCard;
            if (MagicManager.Instance.attackWaiting)
            {
                MagicManager.Instance.waitingTarget = gameObject;
                playerSite = GameContext.Instance.GetCurrentPlayer().playerSite;
            }
        }

        /// <summary>
        /// 鼠标移出时更新等待召唤格子
        /// </summary>
        void MyOnMouseExit(BaseEventData data)
        {
            AttackManager.Instance.waitingTarget = null;
        }
        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求和目标选中
        /// </summary>
        /// <param name="data"></param>
        private void MyOnMouseDown(BaseEventData data)
        {
            Debug.Log("SpellCard Mouse Down");
            // 发布攻击请求消息
            MessageManager.Instance.Dispatch(MessageArea.Magic, MessageEvent.MagicEvent.MagicRequest, gameObject);
            Debug.Log("Try Magic Attack Confirm");
            if (MagicManager.Instance.attackWaiting)
            {
                // 发布攻击确认消息
                MessageManager.Instance.Dispatch(MessageArea.Magic, MessageEvent.MagicEvent.MagicConfirm, gameObject);
            }
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch (areaCode)
            {
                case MessageArea.Attack:
                    switch (eventCode)
                    {
                        case MessageEvent.MagicEvent.MagicRequest:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.MagicEvent.MagicRequest, message as GameObject);
                            break;
                        case MessageEvent.MagicEvent.MagicConfirm:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.MagicEvent.MagicConfirm, message as GameObject);
                            break;
                    }
                    break;
            }
        }
    }
}
