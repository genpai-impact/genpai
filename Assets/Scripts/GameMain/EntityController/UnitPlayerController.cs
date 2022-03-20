using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class UnitPlayerController : MonoBehaviour
    {
        public GenpaiPlayer player;

        /// <summary>
        /// 鼠标移入时更新等待召唤格子
        /// 为啥没动静
        /// </summary>
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
            if (GameContext.CurrentPlayer != GameContext.LocalPlayer)
            {
                return;
            }
            UnitEntity unit = GetComponent<UnitEntity>();
            // 位于玩家回合、选中己方单位、单位可行动
            // todo 全部重构，这部分代码过于混乱，鼠标点击应该是一个纯粹的事件，目前控制点击的脚本太多了。
            if (unit.ownerSite == GameContext.LocalPlayer.playerSite)
            {
                //选中己方格子是判断是治疗还是请求攻击
                if (MagicManager.Instance.cureWaiting == true)
                {
                    MagicManager.Instance.CureConfirm(gameObject);
                }
                //如果不是治疗就判断能不能攻击
                else if(unit.ActionState[UnitState.ActiveAttack] == true)
                {
                    AttackManager.Instance.AttackRequest(gameObject);
                }
            }

            // 位于玩家回合、选中敌方单位
            if (unit.ownerSite != GameContext.LocalPlayer.playerSite)
            {
                if (AttackManager.Instance.attackWaiting)
                {
                    // 发布攻击确认消息
                    AttackManager.Instance.AttackConfirm(gameObject);
                }
                // 还有一个技能/魔法攻击的流程
                else if (MagicManager.Instance.magicAttackWaiting)
                {
                    MagicManager.Instance.MagicAttackConfirm(gameObject);
                }
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
                AttackManager.Instance.AttackConfirm(AttackManager.Instance.waitingTarget);
            }
        }
    }
}