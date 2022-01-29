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

    /// <summary>
    /// 伤害效果结构体
    /// </summary>
    public class Damage : IEffect
    {
        /// <summary>
        /// 抽象伤害来源
        /// 魔法卡等伤害默认由站场角色造成
        /// </summary>
        public UnitEntity source;

        /// <summary>
        /// 具体伤害目标
        /// </summary>
        public UnitEntity target;

        /// <summary>
        /// 具体伤害结构
        /// </summary>
        public DamageStruct damage;

        // TODO：待添加标识，即攻击行为与伤害对应动画
        // 或者Damage和动画组成元组传入效果管理器

        public Damage(UnitEntity _source, UnitEntity _target, DamageStruct _damage)
        {
            source = _source;
            target = _target;
            damage = _damage;
            // 不直接source.GetDamage()，考虑Skill等主动创建形式
        }

        public UnitEntity GetSource()
        {
            return source;
        }

        public UnitEntity GetTarget()
        {
            return target;
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


        public DamageStruct(int _ATK, ElementEnum _Element)
        {
            this.DamageValue = _ATK;
            this.Element = _Element;
        }
    }

}