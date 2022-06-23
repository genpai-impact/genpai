﻿using BattleSystem.Controller.UI;
using BattleSystem.Service.Common;
using BattleSystem.Service.MessageDatas;
using BattleSystem.Service.Player;
using DataScripts.Card;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utils;

namespace BattleSystem.Controller.EntityController
{
    /// <summary>
    /// 卡牌于手牌中时行为层
    /// 象征玩家对手牌的控制/使用
    /// </summary>
    public class CardPlayerController : BaseClickHandle
    {
        public BattleSite playerSite;
        public Card Card;

        public Vector3 startPos;

        private void Awake()
        {
            InitTrigger();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ClickManager.CancelAllClickAction();
            }
        }

        /// <summary>
        /// 初始化鼠标事件触发器
        /// </summary>
        public void InitTrigger()
        {

            UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyOnMouseDown);
            EventTrigger.Entry myBeginDrag = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            myBeginDrag.callback.AddListener(click);

            // 将自身方法注册为UnityAction
            UnityAction<BaseEventData> drag = new UnityAction<BaseEventData>(MyOnMouseDrag);
            // 创建对应事件触发器
            EventTrigger.Entry myDrag = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag
            };
            myDrag.callback.AddListener(drag);

            UnityAction<BaseEventData> afterDrag = new UnityAction<BaseEventData>(MyOnMouseAfterDrag);
            EventTrigger.Entry myAfterDrag = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
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
            GenpaiMouseDown();
        }

        protected override void DoGenpaiMouseDown()
        {
            AudioManager.Instance.PlayerEffect("Battle_CardClick");
            startPos = transform.localPosition;
            // 实现召唤请求
            if (Card is UnitCard)
            {
                SummonManager.Instance.SummonRequest(gameObject);
            }
            else if (Card is SpellCard)
            {
                UsingSpellCard();
            }
        }
        
        
        public void UsingSpellCard()
        {
            if (GameContext.GetPlayerBySite(playerSite).Chara == null)
            {
                Debug.Log("当前没有角色在场，不应该使用魔法卡");
                return;
            }
            SpellManager.Instance.SpellRequest(gameObject);
        }

        /// <summary>
        /// 鼠标拖动事件触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDrag(BaseEventData data)
        {
            gameObject.GetComponent<CardDisplay>().Revert();
            // TODO：优化实现鼠标卡牌相对位置拖拽
            Vector3 mousePosition = new Vector3(
                Input.mousePosition.x - Screen.width / 2,
                Input.mousePosition.y - Screen.height / 5, 
                0);
            transform.localPosition = mousePosition;
        }

        /// <summary>
        /// 鼠标拖动事件松开触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseAfterDrag(BaseEventData data)
        {
            // 未拖动则不执行
            if (Vector3.Distance(transform.localPosition, startPos) < 1)
            {
                return;
            }

            CardAniController cardAniController = GetComponent<CardAniController>();
            cardAniController.MoveTo(new MoveToData(gameObject, cardAniController.targetPosition));

            if (Card is UnitCard)
            {
                SummonAfterDrag();
            }
        }

        private void SummonAfterDrag()
        {
            // 未进入召唤流程
            if (SummonManager.Instance.waitingBucket == null)
            {
                SummonManager.Instance.SummonCancel();
                return;
            }
            // 完成召唤确认
            SummonManager.Instance.SummonConfirm();
        }
    }
}