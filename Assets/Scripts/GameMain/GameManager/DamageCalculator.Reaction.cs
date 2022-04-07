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
        static void Superconduct(UnitEntity source, UnitEntity target)
        {
            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();


            newEffect.Add(new DelBuff(source, target, BuffEnum.Armor));
            newEffect.Add(new DelBuff(source, target, BuffEnum.Shield));
            // 对自己造成无元素伤害
            newEffect.Add(new ReactionDamage(source, target, new DamageStruct(1, ElementEnum.Cryo, false)));

            foreach (GameObject bucket in neighbors)
            {
                UnitEntity newTarget = bucket.GetComponent<BucketEntity>().unitCarry;

                if (newTarget != null)
                {
                    // 先卸甲
                    newEffect.Add(new DelBuff(source, newTarget, BuffEnum.Armor));
                    newEffect.Add(new DelBuff(source, newTarget, BuffEnum.Shield));
                    // 一点AOE冰伤
                    newEffect.Add(new ReactionDamage(source, newTarget, new DamageStruct(1, ElementEnum.Cryo)));
                }
            }

            EffectManager.Instance.InsertTimeStep(newEffect);

        }

        /// <summary>
        /// 超载反应
        /// </summary>
        static void Overload(UnitEntity source, UnitEntity target)
        {
            // 获取周围格子实现超载AOE
            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();

            // 对自己造成二点火伤
            newEffect.Add(new ReactionDamage(source, target, new DamageStruct(1, ElementEnum.Pyro)));

            foreach (GameObject bucket in neighbors)
            {
                UnitEntity newTarget = bucket.GetComponent<BucketEntity>().unitCarry;

                if (newTarget != null)
                {
                    // 二点AOE火伤
                    newEffect.Add(new ReactionDamage(source, newTarget, new DamageStruct(1, ElementEnum.Pyro)));
                }
            }
            EffectManager.Instance.InsertTimeStep(newEffect);
        }

        /// <summary>
        /// 感电反应
        /// </summary>
        static void ElectroCharge(UnitEntity source, UnitEntity target)
        {
            // 追加感电状态
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(source, target, new ElectroChargeBuff()) });
        }

        /// <summary>
        /// 冻结反应
        /// </summary>
        static void Freeze(UnitEntity source, UnitEntity target)
        {
            //追加冻结状态
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(source, target, new FreezeBuff()) });
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
        }

        /// <summary>
        /// 蒸发反应
        /// </summary>
        static void Vaporise(ref Damage damage)
        {
            damage.damageStructure.DamageValue *= 2;
        }

        /// <summary>
        /// 扩散反应
        /// </summary>
        static void Swirl(UnitEntity source, UnitEntity target)
        {
            ElementEnum targetAttach = target.ElementAttachment.ElementType;
            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();

            //newEffect.Add(new Damage(source, target, new DamageStruct(1, targetAttach)));
            newEffect.Add(new ReactionDamage(source, target, new DamageStruct(1, targetAttach)));

            foreach (GameObject bucket in neighbors)
            {
                UnitEntity newTarget = bucket.GetComponent<BucketEntity>().unitCarry;

                if (newTarget != null)
                {
                    //一点扩散伤害
                    // newEffect.Add(new Damage(source, newTarget, new DamageStruct(1, targetAttach)));
                    newEffect.Add(new ReactionDamage(source, newTarget,new DamageStruct(1, targetAttach)));
                }

            }

            EffectManager.Instance.InsertTimeStep(newEffect);
        }

        /// <summary>
        /// 结晶反应
        /// </summary>
        static void Crystallise(UnitEntity source, UnitEntity target)
        {
            // 结晶，给攻击方添加4点护盾
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(null, source, new ShieldBuff(4)) });
            // 遏制超模补丁，未确认开启
            // EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(null, source, new Shield(4)) }, true);
        }
    }
}