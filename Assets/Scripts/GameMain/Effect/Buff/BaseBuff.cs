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

        /// <summary>
        /// Buff作用目标
        /// </summary>
        public UnitEntity target;

        /// <summary>
        /// Buff当前是否生效
        /// </summary>
        public bool trigger = false;

        /// <summary>
        /// Buff与单位相互绑定
        /// </summary>
        /// <param name="_target">待绑定单位</param>
        public virtual void AddBuff(UnitEntity _target)
        {
            target = _target;
            target.buffAttachment.Add(this);
            trigger = true;
        }
    }

    /// <summary>
    /// 受击承伤类Buff
    /// </summary>
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

    /// <summary>
    /// 持续伤害类Buff
    /// </summary>
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

    /// <summary>
    /// 状态影响类Buff
    /// </summary>
    public abstract class StateEffectBuff : BaseBuff, IMessageReceiveHandler
    {
        // 生命周期
        public int LifeCycles;

        /// <summary>
        /// 具体影响附着单位ActionState
        /// 参数方便快捷开关
        /// </summary>
        public virtual void EffectState(bool force = false) { }

        /// <summary>
        /// 己方回合开始时生效
        /// </summary>
        public void Effect(BattleSite site)
        {
            if (trigger && target.ownerSite == site)
            {
                EffectState();
                LifeCycles--;
            }
        }

        /// <summary>
        /// 己方回合结束后判断是否移除buff
        /// </summary>
        public virtual void CheckRemoval(BattleSite site) { }

        /// <summary>
        /// 订阅生命周期刷新时间or销毁时间
        /// 注：所有判定需要加Trigger，以防在buff注销后持续生效
        /// </summary>
        public virtual void Subscribe()
        {
            // 设置玩家行动前生效
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRound, Effect);
            // 设置回合结束时检测销毁
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundEnd, CheckRemoval);
        }
    }

}
