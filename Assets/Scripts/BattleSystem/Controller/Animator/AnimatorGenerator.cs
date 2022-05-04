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
            GenerateSource(TimeStepEffect, ref animatorTimeStep);

            // 设定TargetAnimator
            foreach (IEffect effect in TimeStepEffect.EffectList)
            {
                // 添加受害者
                animatorTimeStep.AddTargetAnimator(
                    GenerateTargetAnimatorByEffect(effect));
            }

            // 设定SpecialAnimator
            GenerateSpecials(TimeStepEffect, ref animatorTimeStep);

            return animatorTimeStep;
        }
        public static void GenerateSource(EffectTimeStep TimeStepEffect, ref AnimatorTimeStep animatorTimeStep)
        {
            switch (TimeStepEffect.effectType)
            {
                // 这仨是要加Source的
                case TimeEffectType.Attack:
                    animatorTimeStep.SetSourceAnimator(
                        new AttackAnimator(TimeStepEffect.GetSourceUnit(),
                        (Damage)TimeStepEffect.EffectList[0]));
                    break;
                case TimeEffectType.Skill:
                    animatorTimeStep.SetSourceAnimator(
                        new SpellAnimator(TimeStepEffect.GetSourceUnit()));
                    break;
                case TimeEffectType.Spell:
                    animatorTimeStep.SetSourceAnimator(
                        new SpellAnimator(TimeStepEffect.GetSourceUnit()));
                    break;
                default:
                    break;
            }
        }

        public static void GenerateSpecials(EffectTimeStep TimeStepEffect, ref AnimatorTimeStep animatorTimeStep)
        {
            switch (TimeStepEffect.effectType)
            {
                case TimeEffectType.Reaction:
                    // 如果时间步为反应类型，增加对首Effect目标创建SpecialAnimator
                    animatorTimeStep.AddSpecialAnimator(
                        ReactionAnimator.GenerateReactionAnimator(
                            TimeStepEffect.GetMainTargetUnit(),
                            (ElementReactionEnum)TimeStepEffect.appendix
                        )
                    );
                    break;
                default:
                    break;
            }
        }

        public static ITargetAnimator GenerateTargetAnimatorByEffect(IEffect Effect)
        {
            switch (Effect.GetType().Name)
            {
                case "AddBuff":
                    return new AddBuffAnimator(Effect.GetTarget());
                case "DelBuff":
                    return new DelBuffAnimator(Effect.GetTarget());
                case "Damage":
                    if (CheckNullDamage(Effect as Damage))
                    {
                        return null;
                    }
                    return new HittenAnimator(Effect.GetTarget(), Effect as Damage);
                case "Cure":
                    return new CureAnimator(Effect.GetTarget(), Effect as Cure);
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
                animatorTimeStep.AddTargetAnimator(new FallAnimator(unit));
            }

            return animatorTimeStep;
        }

        public static AnimatorTimeStep GenerateSummonTimeStep(GameObject unitObject, Unit summonUnit)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            animatorTimeStep.AddTargetAnimator(new SummonAnimator(summonUnit, unitObject));
            return animatorTimeStep;
        }

        public static AnimatorTimeStep GenerateUITimeStep(Unit UIUnit)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            animatorTimeStep.AddTargetAnimator(new UIAnimator(UIUnit));
            return animatorTimeStep;
        }

    }
}