﻿using BattleSystem.Controller.Bucket;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using BattleSystem.Service.Unit;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace BattleSystem.Controller.Unit
{
    public class UnitPlayerController : BaseClickHandle
    {
        public GenpaiPlayer player;

        /// <summary>
        /// 鼠标移入时更新等待召唤格子
        /// 为啥没动静
        /// </summary>
        void OnMouseEnter()
        {
            //Debug.Log("PointerEnter");
            if (AttackManager.Instance.AttackWaiting)
            {
                AttackManager.Instance.WaitingTarget = gameObject;
            }
        }

        /// <summary>
        /// 鼠标移出时更新等待召唤格子
        /// </summary>
        void OnMouseExit()
        {
            AttackManager.Instance.WaitingTarget = null;
        }

        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求和目标选中
        /// </summary>
        private void OnMouseDown()
        {
            GenpaiMouseDown();
        }

        protected override void DoGenpaiMouseDown()
        {
            if (GameContext.CurrentPlayer != GameContext.LocalPlayer)
            {
                return;
            }
            AudioManager.Instance.PlayerEffect("Battle_NormalChoice");
            
            UnitEntity unitE = GetComponent<UnitEntity>();
            Service.Unit.Unit unit = BattleFieldManager.Instance.GetBucketBySerial(unitE.carrier.serial).unitCarry;

            if (unit == null)
            {
                unitE.carrier.GetComponent<BucketPlayerController>().FuckingSummonCombo();
                return;
            }

            // todo 全部重构，这部分代码过于混乱，鼠标点击应该是一个纯粹的事件，目前控制点击的脚本太多了。

            // 位于玩家回合、选中己方单位、单位可行动
            if (unit.OwnerSite == GameContext.LocalPlayer.playerSite)
            {
                if (SpellManager.Instance.IsWaiting)
                {
                    SpellManager.Instance.SpellConfirm(gameObject.GetComponent<UnitEntity>());  // 添上这一行大概能解决魔法卡对己方单位无法生效的问题
                }
                else if (SkillManager.Instance.IsWaiting)
                {
                    SkillManager.Instance.SkillConfirm(gameObject.GetComponent<UnitEntity>());
                }
                else if (unit.ActionState[UnitState.ActiveAttack] == true)
                {
                    // 攻击请求
                    AttackManager.Instance.AttackRequest(gameObject);
                }
            }

            // 位于玩家回合、选中敌方单位
            if (unit.OwnerSite != GameContext.LocalPlayer.playerSite)
            {
                if (SpellManager.Instance.IsWaiting)
                {
                    // 魔法确认
                    SpellManager.Instance.SpellConfirm(gameObject.GetComponent<UnitEntity>());
                }
                else if (SkillManager.Instance.IsWaiting)
                {
                    // 技能确认
                    SkillManager.Instance.SkillConfirm(gameObject.GetComponent<UnitEntity>());
                }
                else if (AttackManager.Instance.AttackWaiting)
                {
                    // 发布攻击确认消息
                    AttackManager.Instance.AttackConfirm(gameObject);
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
            if (AttackManager.Instance.WaitingTarget == null)
            {

            }
            // 完成攻击确认
            else
            {
                AttackManager.Instance.AttackConfirm(AttackManager.Instance.WaitingTarget);
            }
        }
    }
}