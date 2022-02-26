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
        SpellCard spell;
        public BattleSite playerSite;
        void OnMouseEnter()
        {
            //Debug.Log("PointerEnter");
            if (AttackManager.Instance.attackWaiting)
            {
                AttackManager.Instance.waitingTarget = gameObject;
            }
        }

        /// <summary>
        /// 鼠标移出时更新等待召唤格子
        /// </summary>
        void OnMouseExit()
        {
            AttackManager.Instance.waitingTarget = null;
        }
        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求和目标选中
        /// </summary>
        /// <param name="data"></param>
        private void OnMouseDown()
        {
            Debug.Log("SpellCard Mouse Down");
            // 发布攻击请求消息
            MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackRequest, gameObject);
            spell = GetComponent<CardDisplay>().card as SpellCard;
            int targetNum = spell.targetNum;
            if (targetNum == 0)
            {
                //立即打出
            }
            for(int target = 0; target < targetNum; target++)
            {
                Debug.Log("Try Attack Confirm");
                if (AttackManager.Instance.attackWaiting)
                {
                    // 发布攻击确认消息
                    MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackConfirm, gameObject);
                }
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
    }
}
