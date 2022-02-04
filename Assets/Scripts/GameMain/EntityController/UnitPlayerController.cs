using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class UnitPlayerController : MonoBehaviour, IMessageSendHandler
    {
        public GenpaiPlayer player;

        /// <summary>
        /// 鼠标移入时更新等待召唤格子
        /// 为啥没动静
        /// </summary>
        void OnMouseEnter()
        {
            // Debug.Log("PointerEnter");
            if (AttackManager.Instance.attackWaiting)
            {
                AttackManager.Instance.waitingTarget = gameObject;
                // Debug.Log(AttackManager.Instance.waitingTarget.GetComponent<UnitEntity>().unit.unitName);
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


        }



        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求和目标选中
        /// </summary>
        /// <param name="data"></param>
        private void OnMouseDown()
        {
            Debug.Log("Unit Mouse Down");
            UnitEntity unit = GetComponent<UnitEntity>();

            try
            {
                Debug.Log("攻击信息："
                    + " 当前玩家：" + GameContext.CurrentPlayer.playerSite
                    + " 本地玩家：" + GameContext.LocalPlayer.playerSite
                    + " 单位归属：" + unit.owner.playerSite
                    + " 行动状态：" + unit.actionState);
            }
            catch
            {
                Debug.Log("It is FUCKING BOSS");
            }




            // 位于玩家回合、选中己方单位、单位可行动
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner == GameContext.LocalPlayer &&
                unit.actionState == true)
            {
                Debug.Log("Try Attack Request");
                // 发布攻击请求消息
                MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackRequest, gameObject);
            }

            // 位于玩家回合、选中敌方单位
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner != GameContext.LocalPlayer)
            {
                Debug.Log("Try Attack Confirm");
                if (AttackManager.Instance.attackWaiting)
                {
                    // 发布攻击确认消息
                    MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackConfirm, gameObject);
                }
                // 还有一个技能/魔法攻击的流程
            }

        }




        /// <summary>
        /// 鼠标拖动过程触发
        /// 攻击选择需求更新箭头
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDrag(BaseEventData data)
        {
            Debug.Log("Mouse Drag");
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


    }
}