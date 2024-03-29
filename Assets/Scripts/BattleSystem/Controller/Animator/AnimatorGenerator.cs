﻿using System.Collections.Generic;
using BattleSystem.Controller.Animator.SourceAnimators;
using BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator;
using BattleSystem.Controller.Animator.TargetAnimators;
using BattleSystem.Service.Effect;
using BattleSystem.Service.Element;
using UnityEngine;

namespace BattleSystem.Controller.Animator
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
            switch (TimeStepEffect.EffectType)
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
            switch (TimeStepEffect.EffectType)
            {
                case TimeEffectType.Reaction:
                    // 如果时间步为反应类型，增加对首Effect目标创建SpecialAnimator
                    animatorTimeStep.AddSpecialAnimator(
                        ReactionAnimator.GenerateReactionAnimator(
                            TimeStepEffect.GetMainTargetUnit(),
                            (ElementReactionEnum)TimeStepEffect.Appendix
                        )
                    );
                    break;
                default:
                    break;
            }
            MakeAmpSpecial(TimeStepEffect, ref animatorTimeStep);

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
        /// 增幅反应特效，特殊情况特殊处理
        /// </summary>
        public static void MakeAmpSpecial(EffectTimeStep TimeStepEffect, ref AnimatorTimeStep animatorTimeStep)
        {
            foreach (IEffect effect in TimeStepEffect.EffectList)
            {
                if (!(effect is Damage))
                {
                    return;
                }

                Damage damage = effect as Damage;

                if (damage.DamageReaction == ElementReactionEnum.Melt ||
                    damage.DamageReaction == ElementReactionEnum.Vaporise)
                {
                    animatorTimeStep.AddSpecialAnimator(
                        ReactionAnimator.GenerateReactionAnimator(
                            damage.GetTarget(),
                            damage.DamageReaction)
                        );
                }

            }
        }

        /// <summary>
        /// 检查跳过空伤害
        /// </summary>
        public static bool CheckNullDamage(Damage damage)
        {
            return !(damage.DamageStructure.DamageValue > 0);
        }

        public static AnimatorTimeStep GenerateFallTimeStep(List<Service.Unit.Unit> fallUnits)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            foreach (Service.Unit.Unit unit in fallUnits)
            {
                animatorTimeStep.AddTargetAnimator(new FallAnimator(unit));
            }

            return animatorTimeStep;
        }

        public static AnimatorTimeStep GenerateSummonTimeStep(GameObject unitObject, Service.Unit.Unit summonUnit)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            animatorTimeStep.AddTargetAnimator(new SummonAnimator(summonUnit, unitObject));
            return animatorTimeStep;
        }

        public static AnimatorTimeStep GenerateUITimeStep(Service.Unit.Unit unit)
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            animatorTimeStep.AddTargetAnimator(new UIAnimator(unit));
            return animatorTimeStep;
        }

    }
}