using System.Collections.Generic;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Effect;
using BattleSystem.Service.Element;

namespace BattleSystem.Service
{
    /// <summary>
    /// 伤害计算器的元素反应方法部分
    /// </summary>
    public partial class DamageCalculator
    {
        /// <summary>
        /// 超导反应
        /// </summary>
        static void Superconduct(Unit.Unit source, Unit.Unit target)
        {

            List<Bucket> neighbors = BattleFieldManager.Instance.GetNeighbors(target.Carrier);

            List<IEffect> newEffectDelBuff = new List<IEffect>();
            List<IEffect> newEffectDamage = new List<IEffect>();

            newEffectDelBuff.Add(new DelBuff(source, target, 600));
            newEffectDelBuff.Add(new DelBuff(source, target, 601));
            // 对目标造成无元素伤害
            newEffectDamage.Add(new Damage(source, target, new DamageStruct(1, ElementEnum.Cryo, false), DamageType.Reaction));

            foreach (Bucket bucket in neighbors)
            {
                Unit.Unit newTarget = bucket.unitCarry;

                if (newTarget != null)
                {
                    // 先卸甲
                    newEffectDelBuff.Add(new DelBuff(source, newTarget, 600));
                    newEffectDelBuff.Add(new DelBuff(source, newTarget, 601));
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
        static void Overload(Unit.Unit source, Unit.Unit target)
        {
            // 获取周围格子实现超载AOE
            List<Bucket> neighbors = BattleFieldManager.Instance.GetNeighbors(target.Carrier);
            List<IEffect> newEffect = new List<IEffect>();

            // 对自己造成二点火伤
            newEffect.Add(new Damage(source, target, new DamageStruct(2, ElementEnum.Pyro, false), DamageType.Reaction));

            foreach (Bucket bucket in neighbors)
            {
                Unit.Unit newTarget = bucket.unitCarry;

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
        static void ElectroCharge(Unit.Unit source, Unit.Unit target)
        {
            // Debug.Log("感电");
            // 追加感电状态

            EffectManager.Instance.InsertTimeStep(
                new EffectTimeStep(new List<IEffect> { new AddBuff(source, target, 604) },
                TimeEffectType.Reaction,
                ElementReactionEnum.ElectroCharge));

        }

        /// <summary>
        /// 冻结反应
        /// </summary>
        static void Freeze(Unit.Unit source, Unit.Unit target)
        {
            // Debug.Log("冻结");
            // 追加冻结状态
            EffectManager.Instance.InsertTimeStep(
                new EffectTimeStep(new List<IEffect> { new AddBuff(source, target, 603) },
                TimeEffectType.Reaction,
                ElementReactionEnum.Freeze));

        }

        /// <summary>
        /// 融化反应
        /// </summary>
        static void Melt(ref Damage damage)
        {
            ElementEnum AttackElement = damage.DamageStructure.Element;
            if (AttackElement == ElementEnum.Pyro)
            {
                damage.DamageStructure.DamageValue *= 2;
            }
            else
            {
                damage.DamageStructure.DamageValue = (int)(damage.DamageStructure.DamageValue * 1.5);
                // target.unit.BaseATK--; // 后续以挂Buff形式实现
            }
            damage.DamageType = DamageType.Reaction;
            damage.DamageReaction = ElementReactionEnum.Melt;
        }

        /// <summary>
        /// 蒸发反应
        /// </summary>
        static void Vaporise(ref Damage damage)
        {
            damage.DamageStructure.DamageValue *= 2;
            damage.DamageType = DamageType.Reaction;
            damage.DamageReaction = ElementReactionEnum.Vaporise;
        }

        /// <summary>
        /// 扩散反应
        /// </summary>
        static void Swirl(Unit.Unit source, Unit.Unit target)
        {
            ElementEnum targetAttach = target.SelfElement.ElementType;
            List<Bucket> neighbors = BattleFieldManager.Instance.GetNeighbors(target.Carrier);
            List<IEffect> newEffect = new List<IEffect>();

            newEffect.Add(new Damage(source, target, new DamageStruct(1, targetAttach), DamageType.Reaction));

            foreach (Bucket bucket in neighbors)
            {
                Unit.Unit newTarget = bucket.unitCarry;

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
        static void Crystallise(Unit.Unit source, Unit.Unit target)
        {
            // Debug.Log("结晶");
            // 结晶，给攻击方添加4点护盾
            EffectManager.Instance.InsertTimeStep(
                new EffectTimeStep(new List<IEffect> { new AddBuff(null, source, 601,4) },
                TimeEffectType.Reaction,
                ElementReactionEnum.Freeze));
            // 遏制超模补丁，未确认开启
            // EffectManager.Instance.InsertTimeStep(new List<INewEffect> { new AddBuff(null, source, new Shield(4)) }, true);
        }
    }
}