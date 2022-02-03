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

                // TODO：获取Buff相关过程加伤&元素反应加伤



                // 元素附着在计算过程中实时更新（追加&锁），仅返回伤害值供死亡结算
                return (damage.GetTarget(), DamageValue);
            }

        }

        public ElementReactionEnum CheckReaction(Damage damage)
        {
            UnitEntity target = damage.GetTarget();
            ElementReactionEnum reaction = ElementReactionEnum.None;

            // 判断是否产生元素反应
            if (damage.damageStructure.Element != ElementEnum.None)
            {

                Element elementAttachment = target.ElementAttachment;

                // 不存在附着则追加附着
                if (elementAttachment.ElementType == ElementEnum.None)
                {
                    target.ElementAttachment = new Element(damage.damageStructure.Element);
                }
                // 存在附着那就元素反应
                else
                {

                    // TODO：获取元素反应结果
                    reaction = elementAttachment.ElementReaction(damage.damageStructure.Element);
                    Debug.Log("Taking Reaction:" + reaction);
                }
            }

            return reaction;
        }

    }
}