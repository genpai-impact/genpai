using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Buff种类
    /// 不同Buff种类适用于不同的计算场合
    /// </summary>
    public enum BuffType
    {
        // 常态受击减伤Buff（指护盾护甲——层数值、不自主销毁，需要主动销毁
        DamageReduceBuff,

        // 常态攻击加成Buff（如后续武器——层数值、不自主销毁，需要主动销毁
        // DamageEnhanceBuff,

        // 状态更新Buff（如感电、冻结等——无层数、自动影响附着单位、存在生命周期
        StateEffectBuff,

        // 过程计算Buff（指脆弱、天气等——层数值、影响伤害计算过程、存在生命周期

        // Dot伤害Buff（如燃烧等——层数值、回合事件影响、可被主动销毁（可能存在生命周期
        DamageOverTimeBuff,

        // 怪物特性Buff（？）——无层数、特定时间触发、不可销毁、通常不访问

    }



    /// <summary>
    /// Buff基类
    /// </summary>
    public abstract class BaseBuff
    {
        public BuffType buffType;
        public BuffEnum buffName;

        // Buff作用目标
        public UnitEntity target;

        /// <summary>
        /// Buff与单位相互绑定
        /// </summary>
        /// <param name="_target">待绑定单位</param>
        public virtual void AddBuff(UnitEntity _target)
        {
            target = _target;
            target.buffAttachment.Add(this);
        }
    }

    public abstract class DamageReduceBuff : BaseBuff
    {
        // Buff层数
        public int storey;

        /// <summary>
        /// 伤害从此过
        /// </summary>
        /// <param name="damage">进入伤害</param>
        /// <returns>出去伤害</returns>
        public virtual int TakeDamage(int damage) { return damage; }
    }

    public abstract class DamageOverTimeBuff : BaseBuff, IDamageable, IMessageReceiveHandler
    {
        public int DamageValue;
        public ElementEnum DamageElement;

        // Buff层数
        public int storey;

        public DamageStruct GetDamage()
        {
            return new DamageStruct(DamageValue * storey, DamageElement);
        }

        /// <summary>
        /// 订阅Dot实现时间
        /// </summary>
        public virtual void Subscribe() { }
    }

    public abstract class StateEffectBuff : BaseBuff, IMessageReceiveHandler
    {
        // 生命周期
        public int LifeCycles;

        /// <summary>
        /// 如果存活则在特定时间对单位造成特定影响
        /// </summary>
        public virtual void EffectState() { }

        /// <summary>
        /// 己方回合开始时生命周期减一
        /// </summary>
        public virtual void Decrease(BattleSite site)
        {
            if (target.ownerSite == site)
            {
                LifeCycles--;
            }
        }

        /// <summary>
        /// 己方回合结束后判断是否移除buff
        /// </summary>
        public virtual void Remove(BattleSite site)
        {

        }

        /// <summary>
        /// 订阅生命周期刷新时间or销毁时间
        /// </summary>
        public virtual void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, Decrease);
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundEnd, Remove);
        }
    }

}
