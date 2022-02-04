using System.Collections;
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
                CalculateReaction(reaction, ref DamageValue, source, target);


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
        public void CalculateReaction(ElementReactionEnum reaction, ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            switch (reaction)
            {
                case ElementReactionEnum.None:
                    break;
                case ElementReactionEnum.Overload:
                    Overload(ref DamageValue, source, target);
                    // 获取AOE
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Superconduct:
                    Superconduct(ref DamageValue, source, target);
                    // 消除范围盾
                    // 获取AOE
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.ElectroCharge:
                    ElectroCharge(ref DamageValue, source, target);
                    // 获取Buff
                    //EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Freeze:
                    Freeze(ref DamageValue, source, target);
                    // 获取Buff
                    //EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Melt:
                    Melt(ref DamageValue, source, target);
                    break;
                case ElementReactionEnum.Vaporise:
                    Vaporise(ref DamageValue, source, target);
                    break;
                case ElementReactionEnum.Swirl:
                    Swirl(ref DamageValue, source, target);
                    // 获取AOE
                    // EffectManager.Instance.InsertTimeStep(new List<IEffect>());
                    break;
                case ElementReactionEnum.Crystallise:
                    Crystallise(ref DamageValue, source, target);
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

        void Superconduct(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //超导移除护盾及护甲,目标收到1点伤害,冰伤
            List<IEffect> newEffect = new List<IEffect>();
            newEffect.Add(new DelBuff(source, target, BuffEnum.Armor));
            newEffect.Add(new DelBuff(source, target, BuffEnum.Shield));
            EffectManager.Instance.InsertTimeStep(newEffect);

            DamageValue += 1;
        }

        void Overload(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            DamageValue += 2;

            int serial = target.carrier.serial;
            List<GameObject> neighbors = BattleFieldManager.Instance.GetNeighbors(BattleFieldManager.Instance.GetBucketBySerial(serial));
            List<IEffect> newEffect = new List<IEffect>();
            foreach(GameObject obj in neighbors)
            {
                //2点范围火伤并添加火元素附着
                newEffect.Add(new Damage(source, target, new DamageStruct(2, ElementEnum.Pyro)));
                target.ElementAttachment = new Element(ElementEnum.Pyro);
            }
            EffectManager.Instance.InsertTimeStep(newEffect);
        }

        void ElectroCharge(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            List<IEffect> newEffect = new List<IEffect>();
            newEffect.Add(new AddBuff(source,target,BuffEnum.ElectroCharge));
            EffectManager.Instance.InsertTimeStep(newEffect);
        }

        void Freeze(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //TODO:冻结反应
        }

        void Melt(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //TODO:融化反应
        }

        void Vaporise(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //TODO:蒸发反应
        }

        void Swirl(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //TODO:扩散反应
        }

        void Crystallise(ref int DamageValue, UnitEntity source, UnitEntity target)
        {
            //结晶，给攻击方添加4点护盾
            EffectManager.Instance.InsertTimeStep(new List<IEffect> { new AddBuff(null, source, BuffEnum.Shield, 4) });
        }
    }
}