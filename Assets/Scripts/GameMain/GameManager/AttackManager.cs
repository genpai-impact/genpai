using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 攻击管理器，受理攻击请求
    /// 参考SummonManager同BattleFieldManager交互
    /// 攻击管理器确认攻击事件后生成伤害类，并通过EffectManager造成伤害
    /// </summary>
    public class AttackManager : Singleton<AttackManager>, IMessageHandler
    {
        /// <summary>
        /// 等待攻击单位（已发出请求
        /// </summary>
        private GameObject waitingUnit;

        /// <summary>
        /// 请求攻击玩家
        /// </summary>
        public GenpaiPlayer waitingPlayer;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool attackWaiting;

        public GameObject waitingTarget;

        /// <summary>
        /// 攻击请求（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="_sourceUnit">请求攻击游戏对象</param>
        public void AttackRequest(GameObject _sourceUnit)
        {
            if (!attackWaiting)
            {
                attackWaiting = true;

                waitingPlayer = _sourceUnit.GetComponent<UnitEntity>().owner;
                waitingUnit = _sourceUnit;
                bool isRemote = _sourceUnit.GetComponent<UnitEntity>().IsRemote();
                // List<bool> attackHoldList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, isRemote);

                Dispatch(MessageArea.UI, MessageEvent.UIEvent.AttackHighLight);
            }

        }

        /// <summary>
        /// 攻击确认（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="_targetUnit">确认受击游戏对象</param>
        public void AttackConfirm(GameObject _targetUnit)
        {
            if (attackWaiting)
            {
                attackWaiting = false;
                Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight);

                Attack(waitingUnit, _targetUnit);
            }
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

            // 置位攻击来源行动状态
            source.BeActed();

            LinkedList<List<IEffect>> DamageList = MakeAttack(source, target);

            // 将列表传予效果管理器(待改用消息系统实现
            EffectManager.Instance.TakeEffect(DamageList);
        }

        /// <summary>
        /// 创建攻击效果序列
        /// </summary>
        /// <param name="source">攻击者</param>
        /// <param name="target">受击/反击者</param>
        /// <returns>攻击序列</returns>
        public LinkedList<List<IEffect>> MakeAttack(UnitEntity source, UnitEntity target)
        {

            List<IEffect> DamageList = new List<IEffect>();

            // 是否远程攻击（决定是否存在反击
            if (source.IsRemote())
            {
                DamageList.Add(new Damage(source, target, source.GetDamage()));
            }
            else
            {
                DamageList.Add(new Damage(source, target, source.GetDamage()));
                DamageList.Add(new Damage(target, source, target.GetDamage()));
            }

            // 构造传递攻击序列
            LinkedList<List<IEffect>> DamageMessage = new LinkedList<List<IEffect>>();
            DamageMessage.AddLast(DamageList);
            return DamageMessage;
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message = null)
        {
            switch (areaCode)
            {
                case MessageArea.UI:
                    switch (eventCode)
                    {
                        // 
                        case MessageEvent.UIEvent.AttackHighLight:
                            MessageManager.Instance.Dispatch<GameObject>(areaCode, eventCode, message as GameObject);
                            break;
                        case MessageEvent.UIEvent.ShutUpHighLight:
                            MessageManager.Instance.Dispatch<bool>(areaCode, eventCode, true);
                            break;
                    }
                    break;
            }
        }


        public void Subscribe()
        {
            // 订阅单位发布的攻击请求消息
            MessageManager.Instance.GetManager(MessageArea.Attack)
                .Subscribe<GameObject>(MessageEvent.AttackEvent.AttackRequest, AttackRequest);

            // 订阅单位发布的攻击确认消息
            MessageManager.Instance.GetManager(MessageArea.Attack)
                .Subscribe<GameObject>(MessageEvent.AttackEvent.AttackConfirm, AttackConfirm);
        }
    }
}