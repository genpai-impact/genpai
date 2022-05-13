using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 可造成伤害的标识接口
    /// 用于标识单位、魔法卡等可造成伤害来源
    /// </summary>
    public interface IDamageable
    {
        public DamageStruct GetDamage();
    }

    public enum DamageType
    {
        NormalAttack,   //平A
        CounterAttack,  //反击
        RemoteAttack,   //远程
        Magic,          //魔法
        Reaction        //反应
    }

    /// <summary>
    /// 伤害效果结构体
    /// </summary>
    public class Damage : IEffect
    {
        /// <summary>
        /// 抽象伤害来源
        /// 魔法卡等伤害默认由站场角色造成
        /// </summary>
        public readonly Unit Source;

        /// <summary>
        /// 具体伤害目标
        /// </summary>
        public readonly Unit Target;

        /// <summary>
        /// 具体伤害结构
        /// </summary>
        public readonly DamageStruct DamageStructure;

        // TODO：待添加标识，即攻击行为与伤害对应动画
        public DamageType DamageType;

        public ElementReactionEnum DamageReaction = ElementReactionEnum.None;

        public Damage(Unit source, Unit target, DamageStruct damage, DamageType type = DamageType.NormalAttack)
        {
            Source = source;
            Target = target;
            DamageStructure = damage;
            DamageType = type;
        }

        public Unit GetSource()
        {
            return Source;
        }

        public Unit GetTarget()
        {
            return Target;
        }

        public bool ApplyDamage()
        {
            if (DamageStructure.DamageValue == 0)
            {
                return false;
            }

            (int damageValue, bool isFall) = GetTarget().TakeDamage(DamageStructure.DamageValue);
            DamageStructure.DamageValue = damageValue;

            return isFall;

        }
    }

    /// <summary>
    /// 造成伤害的基本结构体
    /// </summary>
    public class DamageStruct
    {
        /// <summary>
        /// 造成的伤害
        /// </summary>
        public int DamageValue;


        /// <summary>
        /// 伤害元素属性
        /// </summary>
        public readonly ElementEnum Element;

        /// <summary>
        /// 是否参与反应
        /// </summary>
        public readonly bool AttendReaction;


        public DamageStruct(int atk, ElementEnum element = ElementEnum.None, bool attendReaction = true)
        {
            DamageValue = atk;
            Element = element;
            AttendReaction = attendReaction;
        }
    }

}