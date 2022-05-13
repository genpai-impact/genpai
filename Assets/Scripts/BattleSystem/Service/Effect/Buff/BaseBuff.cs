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



        // 状态更新Buff（如感电、冻结等——无层数、自动影响附着单位、存在生命周期
        StateEffectBuff,

        // 过程计算Buff（指脆弱、天气等——层数值、影响伤害计算过程、存在生命周期

        // Dot伤害Buff（如燃烧等——层数值、回合事件影响、可被主动销毁（可能存在生命周期
        DamageOverTimeBuff,

        // 怪物特性Buff（？）——无层数、特定时间触发、不可销毁、通常不访问

        ATKEnhanceBuff,

    }

    /// <summary>
    /// Buff基类
    /// </summary>
    public abstract class BaseBuff
    {
        public BuffType BuffType;
        public BuffEnum BuffName;

        /// <summary>
        /// Buff作用目标
        /// </summary>
        public Unit Target;

        /// <summary>
        /// Buff当前是否生效
        /// </summary>
        public bool Trigger = false;

        /// <summary>
        /// Buff与单位相互绑定
        /// </summary>
        /// <param name="target">待绑定单位</param>
        public virtual void AddBuff(Unit target)
        {
            Target = target;
            Target.BuffAttachment.Add(this);
            Trigger = true;
        }
    }
}
