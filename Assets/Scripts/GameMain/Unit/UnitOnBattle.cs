using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位战场行为
    /// 主要为各种情景下点击交互的实现
    /// </summary>
    public class UnitOnBattle : MonoBehaviour, IPointerDownHandler, IMessageSendHandler
    {

        /// <summary>
        /// 点击单位实现攻击请求与确认交互
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            UnitEntity unit = GetComponent<UnitEntity>();

            // 于玩家回合选中己方单位&单位可行动
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner == GameContext.LocalPlayer &&
                unit.actionState == true)
            {
                if (!AttackManager.Instance.attackWaiting)
                {
                    // 发送攻击请求消息
                    Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackRequest, gameObject);
                }

            }

            // 于玩家回合选中敌方单位
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner != GameContext.LocalPlayer)
            {
                if (AttackManager.Instance.attackWaiting)
                {
                    // 发送攻击确认消息
                    Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackConfirm, gameObject);
                }

                // 还有一个技能/魔法目标确认的流程
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
                            MessageManager.Instance.Dispatch<GameObject>(areaCode, eventCode, message as GameObject);
                            break;
                        case MessageEvent.AttackEvent.AttackConfirm:
                            MessageManager.Instance.Dispatch<GameObject>(areaCode, eventCode, message as GameObject);
                            break;
                    }
                    break;
            }
        }

    }
}