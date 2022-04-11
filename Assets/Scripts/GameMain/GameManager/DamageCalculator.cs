using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Genpai
{
    /// <summary>
    /// 伤害计算器
    /// </summary>
    public partial class DamageCalculator : Singleton<DamageCalculator>
    {
        // 计算器线程锁
        public static object calculatorLock = new object();

        /// <summary>
        /// 计算当前伤害
        /// </summary>
        /// <param name="damage">待计算伤害</param>
        /// <returns>元组（伤害，目标）</returns>
        public void Calculate(ref Damage damage)
        {
            lock (calculatorLock)
            {
                Unit source = damage.GetSource();
                Unit target = damage.GetTarget();
                ElementReactionEnum reaction;
                // 进行元素攻击流程
                reaction = TakeReaction(damage);
                damage.damageReaction = reaction;
                // 实现元素反应加伤&事件
                CalculateReaction(reaction, ref damage);
                // TODO：获取Buff相关过程加伤
            }
        }

        /// <summary>
        /// 进行元素反应
        /// </summary>
        /// <param name="damage">伤害事件</param>
        /// <returns>元素反应类型</returns>
        public ElementReactionEnum TakeReaction(Damage damage)
        {
            Unit target = damage.GetTarget();
            Unit source = damage.GetSource();
            ElementReactionEnum reaction = ElementReactionEnum.None;
            // 单位已经死亡
            if (target == null || target.isFall)
            {
                return reaction;
            }

            Element targetAttachment = target.SelfElement;
            ElementEnum damageElement = damage.damageStructure.Element;

            // 判断是否产生元素反应
            if (damageElement != ElementEnum.None && damage.damageStructure.AttendReaction)
            {
                // 不存在附着则追加附着
                if (targetAttachment.ElementType == ElementEnum.None)
                {
                    target.SelfElement = new Element(damage.damageStructure.Element);
                }
                // 存在附着那就元素反应
                else
                {
                    reaction = targetAttachment.ElementReaction(damage.damageStructure.Element);
                }
            }
            // >>> 受元素反应影响Buff检测 
            // 待重构为Element追加Buff，随元素销毁模式
            BaseBuff indexFreeze = target.buffAttachment.FirstOrDefault(buff => buff.buffName == BuffEnum.Freeze);
            if (indexFreeze != null && damageElement == ElementEnum.Pyro)
            {
                //目标处于冻结状态且攻击为火伤
                if (targetAttachment.ElementType == ElementEnum.None)
                {
                    //无元素附着则追加冰附着
                    target.SelfElement = new Element(ElementEnum.Cryo);
                }
                // 去除冻结状态
                // EffectManager.Instance.InsertTimeStep(new List<IEffect> { new DelBuff(source, target, BuffEnum.Freeze) });
            }

            //水元素攻击移除燃烧Buff
            if (damageElement == ElementEnum.Hydro)
            {
                // EffectManager.Instance.InsertTimeStep(new List<IEffect> { new DelBuff(source, target, BuffEnum.Burning, int.MaxValue) });
            }
            // >>>

            return reaction;
        }

        /// <summary>
        /// 执行元素反应
        /// </summary>
        /// <param name="reaction">待执行元素反应</param>
        /// <param name="DamageValue">受元素反应影响的基础伤害值</param>
        public void CalculateReaction(ElementReactionEnum reaction, ref Damage damage)
        {

            Unit source = damage.GetSource();
            Unit target = damage.GetTarget();

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
                    ElectroCharge(source, target);
                    break;
                case ElementReactionEnum.Freeze:
                    Freeze(source, target);
                    break;
                case ElementReactionEnum.Melt:
                    Melt(ref damage);
                    break;
                case ElementReactionEnum.Vaporise:
                    Vaporise(ref damage);
                    break;
                case ElementReactionEnum.Swirl:
                    Swirl(source, target);
                    break;
                case ElementReactionEnum.Crystallise:
                    Crystallise(source, target);
                    break;
            }
            target.SelfElement.FreshLock();
        }
    }
}