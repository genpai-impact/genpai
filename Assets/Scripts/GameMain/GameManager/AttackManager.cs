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

        /// <summary>
        /// 攻击请求（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="_sourceUnit">请求攻击游戏对象</param>
        public void AttackRequest(GameObject _sourceUnit)
        {
            if (true)
            {
                waitingPlayer = _sourceUnit.GetComponent<UnitEntity>().owner;
                waitingUnit = _sourceUnit;
                bool isRemote = _sourceUnit.GetComponent<UnitEntity>().IsRemote();
                List<bool> attackHoldList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer, isRemote);
            }

        }

        /// <summary>
        /// 攻击确认（UnitOnBattle脚本点击获取
        /// </summary>
        /// <param name="_targetUnit">确认受击游戏对象</param>
        public void AttackConfirm(GameObject _targetUnit)
        {
            Attack(waitingUnit, _targetUnit);
        }

        /// <summary>
        /// 执行攻击
        /// </summary>
        /// <param name="_sourceUnit">攻击对象</param>
        /// <param name="_targetUnit">受击对象</param>
        public void Attack(GameObject _sourceUnit, GameObject _targetUnit)
        {
            UnitEntity source = _sourceUnit.GetComponent<UnitEntity>();
            UnitEntity target = _targetUnit.GetComponent<UnitEntity>();

            // 待造成伤害列表(在普通攻击事件中表现为同步的互相攻击)
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

            // 构造传递效果消息
            LinkedList<List<IEffect>> DamageMessage = new LinkedList<List<IEffect>>();
            DamageMessage.AddLast(DamageList);

            // 将列表传予效果管理器(待改用消息系统实现
            EffectManager.Instance.TakeEffect(DamageMessage);
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(string eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }
    }
}