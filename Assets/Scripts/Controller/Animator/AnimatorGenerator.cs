using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// Animator列表制作器，主要由EffectManager调用
    /// </summary>
    public static class AnimatorGenerator
    {
        /// <summary>
        /// 创建通过时间步内效果序列创建动画时间序列
        /// </summary>
        /// <param name="TimeStepEffect"></param>
        /// <returns></returns>
        public static AnimatorTimeStep GenerateAnimatorByEffectTimeStep(EffectTimeStep TimeStepEffect)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();

            // 设定SourceAnimator
            switch (TimeStepEffect.effectType)
            {
                // 这仨是要加Source的
                case TimeEffectType.Attack:
                    animatorTimeStep.SetSourceAnimator(
                        new SourceAnimator(TimeStepEffect.EffectList[0].GetSource(),
                        AnimatorType.SourceAnimator.Attack));
                    break;
                case TimeEffectType.Skill:
                    animatorTimeStep.SetSourceAnimator(
                        new SourceAnimator(TimeStepEffect.EffectList[0].GetSource(),
                        AnimatorType.SourceAnimator.Skill));
                    break;
                case TimeEffectType.Spell:
                    animatorTimeStep.SetSourceAnimator(
                        new SourceAnimator(TimeStepEffect.EffectList[0].GetSource(),
                        AnimatorType.SourceAnimator.Spell));
                    break;
                default:
                    break;
            }

            // 设定TargetAnimator
            foreach (IEffect effect in TimeStepEffect.EffectList)
            {
                // 添加受害者
                animatorTimeStep.AddTargetAnimator(
                    GenerateTargetAnimatorByEffect(effect));
            }

            // TODO：设定特效

            return animatorTimeStep;
        }

        public static ITargetAnimator GenerateTargetAnimatorByEffect(IEffect Effect)
        {
            switch (Effect.GetType().Name)
            {
                case "AddBuff":
                    return new TargetAnimator(Effect.GetTarget(), AnimatorType.TargetAnimator.AddBuff);
                case "DelBuff":
                    return new TargetAnimator(Effect.GetTarget(), AnimatorType.TargetAnimator.DelBuff);
                case "Damage":
                    if (CheckNullDamage(Effect as Damage))
                    {
                        return null;
                    }
                    return new HittenAnimator(Effect.GetTarget(), AnimatorType.TargetAnimator.Hitten, Effect as Damage);
                case "Cure":
                    return new TargetAnimator(Effect.GetTarget(), AnimatorType.TargetAnimator.Cure);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 检查&跳过空伤害
        /// </summary>
        public static bool CheckNullDamage(Damage damage)
        {
            return !(damage.damageStructure.DamageValue > 0);
        }

        public static AnimatorTimeStep GenerateFallTimeStep(List<Unit> fallUnits)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            foreach (Unit unit in fallUnits)
            {
                animatorTimeStep.AddTargetAnimator(new TargetAnimator(unit, AnimatorType.TargetAnimator.Fall));
            }

            return animatorTimeStep;
        }

    }
}