using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using System;

namespace Genpai
{
    /// <summary>
    /// 攻击管理器，受理攻击请求
    /// 参考SummonManager同BattleFieldManager交互
    /// 攻击管理器确认攻击事件后生成伤害类，并通过EffectManager造成伤害
    /// </summary>
    public class AttackManager : Singleton<AttackManager>, IMessageSendHandler
    {
        /// <summary>
        /// 等待攻击单位（已发出请求
        /// </summary>
        private GameObject _waitingUnit;

        /// <summary>
        /// 请求攻击玩家
        /// </summary>
        private BattleSite _waitingPlayer;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool AttackWaiting;

        public GameObject WaitingTarget;

        /// <summary>
        /// 当前(上一次）可攻击列表，每调用CheckAttackable更新一次
        /// 考虑是否在回合开始就载入每个位置的可攻击列表
        /// </summary>
        private List<bool> _attackableList;


        private AttackManager()
        {
            AttackWaiting = false;
        }

        public void Init()
        {

        }

        // 只是为了在GameContextScript中进行新游戏的fresh的时候保持形式同一，没有特殊作用
        public void Fresh()
        {

        }

        /// <summary>
        /// 攻击请求（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="sourceUnit">请求攻击游戏对象</param>
        public void AttackRequest(GameObject sourceUnit)
        {
            if (!AttackWaiting)
            {
                ClickManager.CancelAllClickAction();
                AttackWaiting = true;
                _waitingPlayer = sourceUnit.GetComponent<UnitEntity>().ownerSite;
                _waitingUnit = sourceUnit;

                bool isRemote = sourceUnit.GetComponent<UnitEntity>().GetUnit().IsRemote;
                // 高亮传参
                _attackableList = BattleFieldManager.Instance.CheckAttackable(_waitingPlayer, isRemote);
                Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, _attackableList);
            }
        }

        public void AttackCancel()
        {
            AttackWaiting = false;
        }

        /// <summary>
        /// 攻击确认（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="targetUnit">确认受击游戏对象</param>
        public void AttackConfirm(GameObject targetUnit)
        {
            if (!AttackWaiting)
            {
                return;
            }
            //场上格子的攻击
            if (_attackableList[targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                AttackWaiting = false;
                Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight);
                if (_waitingUnit == null)
                {
                    return;
                }
                Attack(_waitingUnit, targetUnit);
            }
            else
            {
                Debug.Log("你必须先攻击那个具有嘲讽的随从");
            }
        }

        /// <summary>
        /// 执行攻击过程
        /// </summary>
        /// <param name="sourceUnit">攻击对象</param>
        /// <param name="targetUnit">受击对象</param>
        public void Attack(Unit sourceUnit, Unit targetUnit)
        {
            Unit source = BattleFieldManager.Instance.GetBucketBySerial(sourceUnit.Carrier.serial).unitCarry;
            Unit target = BattleFieldManager.Instance.GetBucketBySerial(targetUnit.Carrier.serial).unitCarry;

            // 置位攻击来源行动状态
            source.Acted();
            LinkedList<EffectTimeStep> damageList = MakeAttack(source, target);
            // 将列表传予效果管理器(待改用消息系统实现
            EffectManager.Instance.TakeEffect(damageList);


        }

        /// <summary>
        /// 执行攻击过程
        /// </summary>
        /// <param name="sourceUnit">攻击对象</param>
        /// <param name="targetUnit">受击对象</param>
        private void Attack(GameObject sourceUnit, GameObject targetUnit)
        {
            int sourceSerial = sourceUnit.GetComponent<UnitEntity>().carrier.serial;
            int targetSerial = targetUnit.GetComponent<UnitEntity>().carrier.serial;

            Unit source = BattleFieldManager.Instance.GetBucketBySerial(sourceSerial).unitCarry;
            Unit target = BattleFieldManager.Instance.GetBucketBySerial(targetSerial).unitCarry;


            Attack(source, target);
        }

        /// <summary>
        /// 创建攻击效果序列
        /// </summary>
        /// <param name="source">攻击者</param>
        /// <param name="target">受击/反击者</param>
        /// <returns>攻击序列</returns>
        private static LinkedList<EffectTimeStep> MakeAttack(Unit source, Unit target)
        {
            LinkedList<EffectTimeStep> damageMessage = new LinkedList<EffectTimeStep>();
            // 攻击受击时间错开方案
            // 创建攻击时间步
            List<IEffect> attackList = new List<IEffect> { new Damage(source, target, source.GetDamage()) };
            damageMessage.AddLast(new EffectTimeStep(attackList, TimeEffectType.Attack));
            if (source.IsRemote || target is Boss) return damageMessage;
            
            // 创建反击时间步
            List<IEffect> counterList = new List<IEffect> { new Damage(target, source, target.GetDamage()) };
            damageMessage.AddLast(new EffectTimeStep(counterList, TimeEffectType.Attack));
            return damageMessage;
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message = null)
        {
            switch (areaCode)
            {
                case MessageArea.UI:
                    switch (eventCode)
                    {
                        case MessageEvent.UIEvent.AttackHighLight:
                            MessageManager.Instance.Dispatch<List<bool>>(areaCode, eventCode, message as List<bool>);
                            break;
                        case MessageEvent.UIEvent.ShutUpHighLight:
                            MessageManager.Instance.Dispatch<bool>(areaCode, eventCode, true);
                            break;
                    }
                    break;
            }
        }
    }
}