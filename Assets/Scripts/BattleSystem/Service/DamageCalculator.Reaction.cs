using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 伤害计算器的元素反应方法部分
    /// </summary>
    public partial class DamageCalculator
    {
        /// <summary>
        /// 超导反应
        /// </summary>
        static void Superconduct(Unit source, Unit target)
        {

            List<Bucket> neighbors = BattleFieldManager.Instance.GetNeighbors(target.carrier);

            List<IEffect> newEffectDelBuff = new List<IEffect>();
            List<IEffect> newEffectDamage = new List<IEffect>();

            newEffectDelBuff.Add(new DelBuff(source, target, BuffEnum.Armor));
            newEffectDelBuff.Add(new DelBuff(source, target, BuffEnum.Shield));
            // 对目标造成无元素伤害
            newEffectDamage.Add(new Damage(source, target, new DamageStruct(1, ElementEnum.Cryo, false), DamageType.Reaction));

            foreach (Bucket bucket in neighbors)
            {
                Unit newTarget = bucket.unitCarry;

                if (newTarget != null)
                {
                    // 先卸甲
                    newEffectDelBuff.Add(new DelBuff(source, newTarget, BuffEnum.Armor));
                    newEffectDelBuff.Add(new DelBuff(source, newTarget, BuffEnum.Shield));
                    // 一点AOE冰伤
                    newEffectDamage.Add(new Damage(source, newTarget, new DamageStruct(1, ElementEnum.Cryo), DamageType.Reaction));
                }
            }

            // 先破盾再伤害，所以添加时相反
            EffectManager.Instance.InsertTimeStep(new EffectTimeStep(newEffectDamage, TimeEffectType.Appendix));
            EffectManager.Instance.InsertTimeStep(new EffectTimeStep(newEffectDelBuff, TimeEffectType.Reaction, ElementReactionEnum.Superconduct));
        }

        /// <summary>
        /// 超载反应
        /// </summary>
        static void Overload(Unit source, Unit target)
        {
            // 获取周围格子实现超载AOE
            List<Bucket> neighbors = BattleFieldManager.Instance.GetNeighbors(target.carrier);
            List<IEffect> newEffect = new List<IEffect>();

            // 对自己造成二点火伤
            newEffect.Add(new Damage(source, target, new DamageStruct(2, ElementEnum.Pyro, false), DamageType.Reaction));

            foreach (Bucket bucket in neighbors)
            {
                Unit newTarget = bucket.unitCarry;

                if (newTarget != null)
                {
                    // 二点AOE火伤
                    newEffect.Add(new Damage(source, newTarget, new DamageStruct(2, ElementEnum.Pyro), DamageType.Reaction));
                }
            }

            EffectManager.Instance.InsertTimeStep(new EffectTimeStep(newEffect, TimeEffectType.Reaction, ElementReactionEnum.Overload));
        }

        /// <summary>
        /// 感电反应
        /// </summary>
        static void ElectroCharge(Unit source, Unit target)
        {
            // Debug.Log("感电");
            // 追加感电状态

            EffectManager.Instance.InsertTimeStep(
                new EffectTimeStep(new List<IEffect> { new AddBuff(source, target, new ElectroChargeBuff()) },
                TimeEffectType.Reaction,
                ElementReactionEnum.ElectroCharge));

        }

        /// <summary>
        /// 冻结反应
        /// </summary>
        static void Freeze(Unit source, Unit target)
        {
            // Debug.Log("冻结");
            // 追加冻结状态
            EffectManager.Instance.InsertTimeStep(
                new EffectTimeStep(new List<IEffect> { new AddBuff(source, target, new FreezeBuff()) },
                TimeEffectType.Reaction,
                ElementReactionEnum.Freeze));

        }

        /// <summary>
        /// 融化反应
        /// </summary>
        static void Melt(ref Damage damage)
        {
            ElementEnum AttackElement = damage.damageStructure.Element;
            if (AttackElement == ElementEnum.Pyro)
            {
                damage.damageStructure.DamageValue *= 2;
            }
            else
            {
                damage.damageStructure.DamageValue = (int)(damage.damageStructure.DamageValue * 1.5);
                // target.unit.BaseATK--; // 后续以挂Buff形式实现
            }
            damage.damageType = DamageType.Reaction;
            damage.damageReaction = ElementReactionEnum.Melt;
        }

        /// <summary>
        /// 蒸发反应
        /// </summary>
        static void Vaporise(ref Damage damage)
        {
            damage.damageStructure.DamageValue *= 2;
            damage.damageType = DamageType.Reaction;
            damage.damageReaction = ElementReactionEnum.Vaporise;
        }

        /// <summary>
        /// 扩散反应
        /// </summary>
        static void Swirl(Unit source, Unit target)
        {
            ElementEnum targetAttach = target.SelfElement.ElementType;
            List<Bucket> neighbors = BattleFieldManager.Instance.GetNeighbors(target.carrier);
            List<IEffect> newEffect = new List<IEffect>();

            newEffect.Add(new Damage(source, target, new DamageStruct(1, targetAttach), DamageType.Reaction));

            foreach (Bucket bucket in neighbors)
            {
                Unit newTarget = bucket.unitCarry;

                if (newTarget != null)
                {
                    //一点扩散伤害
                    newEffect.Add(new Damage(source, newTarget, new DamageStruct(1, targetAttach), DamageType.Reaction));
                }
            }

            EffectManager.Instance.InsertTimeStep(new EffectTimeStep(newEffect, TimeEffectType.Reaction, ElementReactionEnum.Swirl));
        }

        /// <summary>
        /// 结晶反应
        /// </summary>
        static void Crystallise(Unit source, Unit target)
        {
            // Debug.Log("结晶");
            // 结晶，给攻击方添加4点护盾
            EffectManager.Instance.InsertTimeStep(
                new EffectTimeStep(new List<IEffect> { new AddBuff(null, source, new ShieldBuff(4)) },
                TimeEffectType.Reaction,
                ElementReactionEnum.Freeze));
            // 遏制超模补丁，未确认开启
            // EffectManager.Instance.InsertTimeStep(new List<INewEffect> { new AddBuff(null, source, new Shield(4)) }, true);
        }
    }
}