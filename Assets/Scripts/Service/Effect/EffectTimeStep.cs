using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于描述时间步所发生事件类型
    /// </summary>
    public enum TimeEffectType
    {
        Attack,             // 描述攻击事件（包含闪现、攻击动画、目标动画、跳伤害、UI更新）
        Spell,              // 描述使用魔法（包含角色魔法起手、目标动画、跳伤害、UI更新）
        Skill,              // 描述使用技能（包含角色技能起手、目标动画、跳伤害、UI更新）

        Reaction,           // 描述元素反应（包含反应特效、目标动画、跳伤害、UI更新）
        Appendix,            // 描述追加效果，如天街巡游后段/米奇妙妙牌（包括目标动画、跳效果、UI更新）
       
        Fixed,              // 固定伤害，如Dot（包括目标动画、跳效果、UI更新）
    }

    /// <summary>
    /// 描述一个时间步内所要实现的效果
    /// </summary>
    public class EffectTimeStep
    {
        /// <summary>
        /// 储存效果列
        /// </summary>
        public List<IEffect> EffectList;

        /// <summary>
        /// 时间步备注
        /// </summary>
        public TimeEffectType effectType = TimeEffectType.Appendix;

        public object appendix;

        public EffectTimeStep(List<IEffect> effects, TimeEffectType _effectType, object _appendix = null)
        {
            EffectList = effects;
            effectType = _effectType;
            appendix = _appendix;
        }

    }
}