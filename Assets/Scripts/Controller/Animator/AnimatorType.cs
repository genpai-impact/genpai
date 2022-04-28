using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于标识IAnimator记录的动画类型
    /// </summary>
    public class AnimatorType
    {
        public enum AnimatorTypeEnum
        {
            SourceAnimator,
            TargetAnimator,
            SpecialAnimator,
        }

        public enum SourceAnimator
        {
            Attack,     // 攻击动画
            Skill,      // 技能动画
            Spell,      // 施法动画
        }

        public enum TargetAnimator
        {
            Summon,     // 召唤生成单位动画
            Fall,       // 死亡销毁单位动画

            Hitten,     // 受击动画
            Cure,       // 治疗动画（无资源

            AddBuff,    // Buff更新动画（无资源
            DelBuff,    // Buff更新动画（无资源

            UI,
        }

        /// <summary>
        /// 额外播出特效的特殊类型
        /// </summary>
        public enum SpecialAnimator
        {
            Reaction,   // 反应特效
            Appendix,   // 技能特效（无资源
            Fixed,      // 其它特效（无资源
        }
    }

}