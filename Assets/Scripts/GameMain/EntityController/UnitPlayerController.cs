﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class UnitPlayerController : MonoBehaviour, IMessageSendHandler, IPointerDownHandler
    {
        public GenpaiPlayer player;

        /// <summary>
        /// 鼠标移入时更新等待召唤格子
        /// </summary>
        void OnMouseEnter()
        {
            if (AttackManager.Instance.attackWaiting)
            {
                AttackManager.Instance.waitingTarget = gameObject;
                Debug.Log(AttackManager.Instance.waitingTarget.GetComponent<UnitEntity>().unit.unitName);
            }
        }

        /// <summary>
        /// 鼠标移出时更新等待召唤格子
        /// </summary>
        void OnMouseExit()
        {
            AttackManager.Instance.waitingTarget = null;
        }


        private void Awake()
        {

            InitTrigger();
        }

        /// <summary>
        /// 初始化鼠标事件触发器
        /// </summary>
        public void InitTrigger()
        {


            // 点击、开始拖动触发器
            UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyOnMouseDown);
            EventTrigger.Entry myBeginDrag = new EventTrigger.Entry();
            myBeginDrag.eventID = EventTriggerType.PointerDown;
            myBeginDrag.callback.AddListener(click);

            // 拖动触发器
            UnityAction<BaseEventData> draging = new UnityAction<BaseEventData>(MyOnMouseDrag);
            EventTrigger.Entry myDrag = new EventTrigger.Entry();
            myDrag.eventID = EventTriggerType.Drag;
            myDrag.callback.AddListener(draging);

            // 拖动结束触发器
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
        /// 攻击请求和目标选中
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDown(BaseEventData data)
        {
            Debug.Log("Mouse Down");
            UnitEntity unit = GetComponent<UnitEntity>();

            // 位于玩家回合、选中己方单位、单位可行动
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner == GameContext.LocalPlayer &&
                unit.actionState == true)
            {
                if (!AttackManager.Instance.attackWaiting)
                {
                    // 发布攻击请求消息
                    MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackRequest, gameObject);
                }
            }

            // 位于玩家回合、选中敌方单位
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner != GameContext.LocalPlayer)
            {
                if (AttackManager.Instance.attackWaiting)
                {
                    // 发布攻击确认消息
                    MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackConfirm, AttackManager.Instance.waitingTarget);
                }
                // 还有一个技能/魔法攻击的流程
            }

        }

        /// <summary>
        /// 鼠标拖动事件触发方法
        /// 攻击选择需求
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDrag(BaseEventData data)
        {
            // Debug.Log("Mouse Drag");
            // TODO：设计攻击选择箭头

        }

        /// <summary>
        /// 鼠标拖动事件松开触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseAfterDrag(BaseEventData data)
        {
            // 若未进入攻击流程，则销毁选择箭头对象
            if (AttackManager.Instance.waitingTarget == null)
            {

            }
            // 完成攻击确认
            else
            {
                MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackConfirm, AttackManager.Instance.waitingTarget);

            }
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch (areaCode)
            {
                case MessageArea.Attack:
                    switch (eventCode)
                    {
                        case MessageEvent.AttackEvent.AttackRequest:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.AttackEvent.AttackRequest, message as GameObject);
                            break;
                        case MessageEvent.AttackEvent.AttackConfirm:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.AttackEvent.AttackConfirm, message as GameObject);
                            break;
                    }
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Debug.Log("Pointer Down");
        }
    }
}