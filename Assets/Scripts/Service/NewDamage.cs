using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{

    /// <summary>
    /// 伤害效果结构体
    /// </summary>
    public class NewDamage : INewEffect
    {
        /// <summary>
        /// 抽象伤害来源
        /// 魔法卡等伤害默认由站场角色造成
        /// </summary>
        public NewUnit source;

        /// <summary>
        /// 具体伤害目标
        /// </summary>
        public NewUnit target;

        /// <summary>
        /// 具体伤害结构
        /// </summary>
        public DamageStruct damageStructure;

        // TODO：待添加标识，即攻击行为与伤害对应动画
        public DamageType damageType;

        public ElementReactionEnum damageReaction = ElementReactionEnum.None;

        public string effectType;

        public NewDamage(NewUnit _source, NewUnit _target, DamageStruct _damage)
        {
            source = _source;
            target = _target;
            damageStructure = _damage;
            effectType = "Damage";
        }

        public NewUnit GetSource()
        {
            return source;
        }

        public NewUnit GetTarget()
        {
            return target;
        }

        public virtual bool ApplyDamage()
        {
            if (damageStructure.DamageValue == 0)
            {
                return false;
            }


            // 受击动画已整合至TakeDamage中
            (int damageValue, bool isFall) = GetTarget().TakeDamage(damageStructure.DamageValue);
            damageStructure.DamageValue = damageValue;


            return isFall;

        }
    }


}