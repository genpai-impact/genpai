using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                CalculateReaction(reaction, ref DamageValue, damage);


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
        public void CalculateReaction(ElementReactionEnum reaction, ref int DamageValue, Damage damage)
        {
            UnitEntity source = damage.GetSource();
            UnitEntity target = damage.GetTarget();
            ElementEnum AttackElement = damage.damageStructure.Element;
            switch (reaction)
            {
                case ElementReactionEnum.None:
                    break;
                case ElementReactionEnum.Overload:
                    Overload(source, target);
                    break;
                case ElementReactionEnum.Superconduct:
                    Superconduct(source, target);
                    break;
                case ElementReactionEnum.ElectroCharge:
                    ElectroCharge(ref DamageValue, source, target);
                    break;
                case ElementReactionEnum.Freeze:
                    Freeze(ref DamageValue, source, target);
                    break;
                case ElementReactionEnum.Melt:
                    Melt(ref DamageValue, AttackElement, target);
                    break;
                case ElementReactionEnum.Vaporise:
                    Vaporise(ref DamageValue);
                    break;
                case ElementReactionEnum.Swirl:
                    Swirl(ref DamageValue, source, target);
                    break;
                case ElementReactionEnum.Crystallise:
                    Crystallise(ref DamageValue, source, target);
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
            UnitEntity source = damage.GetSource();
            ElementReactionEnum reaction = ElementReactionEnum.None;
            Element targetAttachment = target.ElementAttachment;
            ElementEnum damageElement = damage.damageStructure.Element;


            BaseBuff indexFreeze = target.buffAttachment.FirstOrDefault(buff => buff.buffName == BuffEnum.Freeze);

            if (indexFreeze != null && damageElement == ElementEnum.Pyro)
            {
                //目标处于冻结状态且攻击为火伤
                if (targetAttachment.ElementType == ElementEnum.None)
                {
                    //无元素附着则追加冰附着
                    target.ElementAttachment = new Element(ElementEnum.Cryo);
                }
                //去除冻结状态
                EffectManager.Instance.InsertTimeStep(new List<IEffect> { new DelBuff(source, target, BuffEnum.Freeze) });
            }

            //水元素攻击移除燃烧Buff
            if (damageElement == ElementEnum.Hydro)
            {
                EffectManager.Instance.InsertTimeStep(new List<IEffect> { new DelBuff(source, target, BuffEnum.Burning, int.MaxValue) });
            }

            // 判断是否产生元素反应
            if (damage.damageStructure.Element != ElementEnum.None)
            {
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

        void Superconduct(UnitEntity source, UnitEntity target)
        {
            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();

            foreach (GameObject bucket in neighbors)
            {
                UnitEntity newTarget = bucket.GetComponent<BucketEntity>().unitCarry;

                if (newTarget != null)
                {
                    // 先卸甲
                    newEffect.Add(new DelBuff(source, newTarget, BuffEnum.Armor));
                    newEffect.Add(new DelBuff(source, newTarget, BuffEnum.Shield));
                    // 一点AOE冰伤
                    newEffect.Add(new Damage(source, newTarget, new DamageStruct(1, ElementEnum.Cryo)));
                }
            }

            EffectManager.Instance.InsertTimeStep(newEffect);

        }

        void Overload(UnitEntity source, UnitEntity target)
        {
            // 获取周围格子实现超载AOE
            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();

            foreach (GameObject bucket in neighbors)
            {
                UnitEntity newTarget = bucket.GetComponent<BucketEntity>().unitCarry;

                if (newTarget != null)
                {
                    // 二点AOE火伤
                    newEffect.Add(new Damage(source, newTarget, new DamageStruct(2, ElementEnum.Pyro)));
                }

            }

            EffectManager.Instance.InsertTimeStep(newEffect);
        }

        void ElectroCharge(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            // 追加感电状态
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(source, target, new ElectroCharge()) });
        }

        void Freeze(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //追加冻结状态
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(source, target, new Freeze()) });
        }

        void Melt(ref int DamageValue, ElementEnum AttackElement, UnitEntity target)
        {
            if (AttackElement == ElementEnum.Pyro)
            {
                DamageValue *= 2;
            }
            else
            {
                DamageValue = (int)(DamageValue * 1.5);
                target.unit.BaseATK--;
            }
        }

        void Vaporise(ref int DamageValue)
        {
            DamageValue *= 2;
        }

        void Swirl(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            ElementEnum targetAttach = target.ElementAttachment.ElementType;
            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();

            foreach (GameObject bucket in neighbors)
            {
                UnitEntity newTarget = bucket.GetComponent<BucketEntity>().unitCarry;

                if (newTarget != null)
                {
                    //一点扩散伤害
                    newEffect.Add(new Damage(source, newTarget, new DamageStruct(1, targetAttach)));
                }

            }

            EffectManager.Instance.InsertTimeStep(newEffect);
        }

        void Crystallise(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //结晶，给攻击方添加4点护盾
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(null, source, new Shield(4)) });
        }
    }
}