﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 伤害计算器
    /// </summary>
    public class DamageCalculator : Singleton<DamageCalculator>
    {
        // 计算器线程锁
        public static object calculatorLock = new object();

        /// <summary>
        /// 计算当前伤害
        /// </summary>
        /// <param name="damage">待计算伤害</param>
        /// <returns>元组（伤害，目标）</returns>
        public (UnitEntity, int) Calculate(Damage damage)
        {
            lock (calculatorLock)
            {
                UnitEntity source = damage.GetSource();
                UnitEntity target = damage.GetTarget();
                ElementReactionEnum reaction;

                // 进行元素攻击流程
                reaction = CheckReaction(damage);

                int DamageValue = damage.damageStructure.DamageValue;

                // 实现元素反应加伤&事件
                CalculateReaction(reaction, ref DamageValue);


                // TODO：获取Buff相关过程加伤



                // 元素附着在计算过程中实时更新（追加&锁），仅返回伤害值供死亡结算
                return (damage.GetTarget(), DamageValue);
            }

        }

        /// <summary>
        /// 执行元素反应
        /// </summary>
        /// <param name="reaction">待执行元素反应</param>
        /// <param name="DamageValue">受元素反应影响的基础伤害值</param>
        public void CalculateReaction(ElementReactionEnum reaction, ref int DamageValue)
        {
            switch (reaction)
            {
                case ElementReactionEnum.None:
                    break;
                case ElementReactionEnum.Overload:
                    // 获取AOE
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Superconduct:
                    // 消除范围盾
                    // 获取AOE
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.ElectroCharge:
                    // 获取Buff
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Freeze:
                    // 获取Buff
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Burning:
                    // 获取Buff
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Melt:
                    DamageValue *= 2;
                    break;
                case ElementReactionEnum.Vaporise:
                    DamageValue *= 2;
                    break;
                case ElementReactionEnum.Swirl:
                    // 获取AOE
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Crystallise:
                    // 获取Buff
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
            }
        }

        /// <summary>
        /// 进行元素反应
        /// </summary>
        /// <param name="damage">伤害事件</param>
        /// <returns>元素反应类型</returns>
        public ElementReactionEnum CheckReaction(Damage damage)
        {
            UnitEntity target = damage.GetTarget();
            ElementReactionEnum reaction = ElementReactionEnum.None;

            // 判断是否产生元素反应
            if (damage.damageStructure.Element != ElementEnum.None)
            {
                Element targetAttachment = target.ElementAttachment;

                // 不存在附着则追加附着
                if (targetAttachment.ElementType == ElementEnum.None)
                {
                    target.ElementAttachment = new Element(damage.damageStructure.Element);
                }
                // 存在附着那就元素反应
                else
                {
                    reaction = targetAttachment.ElementReaction(damage.damageStructure.Element);
                    Debug.Log("Taking Reaction:" + reaction);
                }
            }

            return reaction;
        }

    }
}