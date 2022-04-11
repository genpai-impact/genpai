﻿using System.Collections;
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
        NormalAttack,
        CounterAttack,
        RemoteAttack,
        Magic,
    }

    /// <summary>
    /// 伤害效果结构体
    /// </summary>
    public class Damage : IEffect
    {
        // <summary>
        /// 抽象伤害来源
        /// 魔法卡等伤害默认由站场角色造成
        /// </summary>
        public Unit source;

        /// <summary>
        /// 具体伤害目标
        /// </summary>
        public Unit target;

        /// <summary>
        /// 具体伤害结构
        /// </summary>
        public DamageStruct damageStructure;

        // TODO：待添加标识，即攻击行为与伤害对应动画
        public DamageType damageType;

        public ElementReactionEnum damageReaction = ElementReactionEnum.None;

        public string effectType;

        public Damage(Unit _source, Unit _target, DamageStruct _damage)
        {
            source = _source;
            target = _target;
            damageStructure = _damage;
            effectType = "Damage";
        }

        public Unit GetSource()
        {
            return source;
        }

        public Unit GetTarget()
        {
            return target;
        }

        public virtual bool ApplyDamage()
        {
            if (damageStructure.DamageValue == 0)
            {
                return false;
            }

            (int damageValue, bool isFall) = GetTarget().TakeDamage(damageStructure.DamageValue);
            damageStructure.DamageValue = damageValue;

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
        public ElementEnum Element;

        /// <summary>
        /// 是否参与反应
        /// </summary>
        public bool AttendReaction;


        public DamageStruct(int _ATK, ElementEnum _Element, bool _AttendReaction = true)
        {
            this.DamageValue = _ATK;
            this.Element = _Element;
            AttendReaction = _AttendReaction;
        }
    }

}