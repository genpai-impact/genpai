﻿using System.Collections;
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
        private GameObject waitingUnit;

        /// <summary>
        /// 请求攻击玩家
        /// </summary>
        public BattleSite waitingPlayer;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool attackWaiting;

        public GameObject waitingTarget;

        /// <summary>
        /// 当前(上一次）可攻击列表，每调用CheckAttackable更新一次
        /// 考虑是否在回合开始就载入每个位置的可攻击列表
        /// </summary>
        public List<bool> atkableList;


        private AttackManager()
        {
            attackWaiting = false;
        }

        public void Init()
        {

        }

        /// <summary>
        /// 攻击请求（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="_sourceUnit">请求攻击游戏对象</param>
        public void AttackRequest(GameObject _sourceUnit)
        {
            if (!attackWaiting)
            {
                ClickManager.Instance.CancelAllClickAction();
                attackWaiting = true;
                waitingPlayer = _sourceUnit.GetComponent<UnitEntity>().ownerSite;
                waitingUnit = _sourceUnit;
                bool isRemote = _sourceUnit.GetComponent<UnitEntity>().IsRemote();
                // 高亮传参
                atkableList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, isRemote);
                Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight, atkableList);
            }
        }

        public void AttackCancel()
        {
            attackWaiting = false;
        }

        /// <summary>
        /// 攻击确认（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="_targetUnit">确认受击游戏对象</param>
        public void AttackConfirm(GameObject _targetUnit)
        {
            if (!attackWaiting)
            {
                return;
            }
            //场上格子的攻击
            if (atkableList[_targetUnit.GetComponent<UnitEntity>().carrier.serial])
            {
                attackWaiting = false;
                Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight);
                if (waitingUnit == null)
                {
                    return;
                }
                Attack(waitingUnit, _targetUnit);
            }
            else
            {
                Debug.Log("你必须先攻击那个具有嘲讽的随从");
            }
        }

        /// <summary>
        /// 执行攻击过程
        /// </summary>
        /// <param name="source">攻击对象</param>
        /// <param name="target">受击对象</param>
        public void Attack(UnitEntity source, UnitEntity target)
        {
            NewUnit _source = NewBattleFieldManager.Instance.GetBucketBySerial(source.carrier.serial).unitCarry;
            NewUnit _target = NewBattleFieldManager.Instance.GetBucketBySerial(target.carrier.serial).unitCarry;

            // 置位攻击来源行动状态
            source.Acted();
            LinkedList<List<IEffect>> DamageList = MakeAttack(source, target);
            // 将列表传予效果管理器(待改用消息系统实现
            EffectManager.Instance.TakeEffect(DamageList);


            LinkedList<List<INewEffect>> NewDamageList = NewMakeAttack(_source, _target);

            NewEffectManager.Instance.TakeEffect(NewDamageList);
        }

        /// <summary>
        /// 执行攻击过程
        /// </summary>
        /// <param name="_sourceUnit">攻击对象</param>
        /// <param name="_targetUnit">受击对象</param>
        public void Attack(GameObject _sourceUnit, GameObject _targetUnit)
        {
            UnitEntity source = _sourceUnit.GetComponent<UnitEntity>();
            UnitEntity target = _targetUnit.GetComponent<UnitEntity>();

            // TODO: 明确音效指定
            AudioManager.Instance.PlayerEffect(1);

            Attack(source, target);
        }

        /// <summary>
        /// 创建攻击效果序列
        /// </summary>
        /// <param name="source">攻击者</param>
        /// <param name="target">受击/反击者</param>
        /// <returns>攻击序列</returns>
        public LinkedList<List<IEffect>> MakeAttack(UnitEntity source, UnitEntity target)
        {
            LinkedList<List<IEffect>> DamageMessage = new LinkedList<List<IEffect>>();
            // 攻击受击时间错开方案
            // 创建攻击时间步
            List<IEffect> AttackList = new List<IEffect>();
            AttackList.Add(new Damage(source, target, source.GetDamage()));
            DamageMessage.AddLast(AttackList);

            // 创建反击时间步
            if (!source.IsRemote())
            {
                List<IEffect> CounterList = new List<IEffect>();
                CounterList.Add(new Damage(target, source, target.GetDamage()));
                DamageMessage.AddLast(CounterList);
                return DamageMessage;
            }
            return DamageMessage;
        }

        public LinkedList<List<INewEffect>> NewMakeAttack(NewUnit source, NewUnit target)
        {
            LinkedList<List<INewEffect>> DamageMessage = new LinkedList<List<INewEffect>>();
            // 攻击受击时间错开方案
            // 创建攻击时间步
            List<INewEffect> AttackList = new List<INewEffect>();

            AttackList.Add(new NewDamage(source, target, source.GetDamage()));
            DamageMessage.AddLast(AttackList);

            // 创建反击时间步
            if (!source.isRemote)
            {
                List<INewEffect> CounterList = new List<INewEffect>();
                CounterList.Add(new NewDamage(target, source, target.GetDamage()));
                DamageMessage.AddLast(CounterList);
            }
            return DamageMessage;
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