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

                // 元素攻击流程
                if (damage.damage.Element != ElementEnum.None)
                {
                    Element elementAttachment = target.ElementAttachment;

                    // 不存在附着则追加附着
                    if (elementAttachment.ElementType == ElementEnum.None)
                    {
                        target.ElementAttachment = new Element(damage.damage.Element);

                    }
                    // 存在附着那就元素反应
                    else
                    {
                        reaction = elementAttachment.ElementReaction(damage.damage.Element);
                        Debug.Log(reaction);
                    }
                }




                // TODO：获取Buff相关过程加伤&元素反应


                // 元素附着在计算过程中实时更新（追加&锁），仅返回伤害值供死亡结算
                return (damage.GetTarget(), damage.damage.DamageValue);
            }

        }

    }
}