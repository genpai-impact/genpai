using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 卡牌于手牌中时行为层
    /// 象征玩家对手牌的控制/使用
    /// </summary>
    public class CardPlayerController : MonoBehaviour, IMessageSendHandler
    {
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
            EventTrigger.Entry myBeginDrag = new EventTrigger.Entry();
            myBeginDrag.eventID = EventTriggerType.PointerDown;
            myBeginDrag.callback.AddListener(click);

            // 将自身方法注册为UnityAction
            UnityAction<BaseEventData> drag = new UnityAction<BaseEventData>(MyOnMouseDrag);
            // 创建对应事件触发器
            EventTrigger.Entry myDrag = new EventTrigger.Entry();
            myDrag.eventID = EventTriggerType.Drag;
            myDrag.callback.AddListener(drag);

            UnityAction<BaseEventData> afterDrag = new UnityAction<BaseEventData>(MyOnMouseAfterDrag);
            EventTrigger.Entry myAfterDrag = new EventTrigger.Entry();
            myAfterDrag.eventID = EventTriggerType.PointerUp;
            myAfterDrag.callback.AddListener(afterDrag);


            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            trigger.triggers.Add(myDrag);
            trigger.triggers.Add(myAfterDrag);
            trigger.triggers.Add(myBeginDrag);
        }

        /// <summary>
        /// 鼠标点击事件触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDown(BaseEventData data)
        {
            Debug.Log("Card Click");
            // 实现召唤请求
            Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonRequest, gameObject);

        }

        /// <summary>
        /// 鼠标拖动事件触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDrag(BaseEventData data)
        {

            // TODO：优化实现鼠标卡牌相对位置拖拽
            Vector3 mousePosition = new Vector3(Input.mousePosition.x - 910, Input.mousePosition.y, 0);

            transform.localPosition = mousePosition;

        }

        /// <summary>
        /// 鼠标拖动事件松开触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseAfterDrag(BaseEventData data)
        {
            // 若未进入召唤流程，则实现返回手牌动画
            if (SummonManager.Instance.waitingBucket == null)
            {
                MoveToData moveMessage = new MoveToData(gameObject, GetComponent<CardAniController>().targetPosition);
                Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, moveMessage);
            }
            // 完成召唤确认
            else
            {
                Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, SummonManager.Instance.waitingBucket);
            }
            // MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch (areaCode)
            {
                case MessageArea.Summon:
                    switch (eventCode)
                    {
                        case MessageEvent.SummonEvent.SummonRequest:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonRequest, message as GameObject);
                            break;
                        case MessageEvent.SummonEvent.SummonConfirm:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, message as GameObject);
                            break;
                    }
                    break;
                case MessageArea.Card:
                    switch (eventCode)
                    {
                        case MessageEvent.CardEvent.MoveTo:
                            MessageManager.Instance.Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, message as MoveToData);
                            break;
                    }
                    break;
            }
        }



    }
}