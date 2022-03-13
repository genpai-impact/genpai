using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 魔法种类
    /// </summary>
    public enum SpellType
    {
        Damage,     //伤害
        Cure,       //治疗
        Draw,       //过牌
        Buff,       //Buff
        Weather,    //天气
    }

    public enum SpellElementBuff
    {
        ElementDamage,  // 元素增强 + 数据类型ElementEnum
        AreaChange,     // 范围增强 + 数据类型TargetInfo
        NumericalBuff   // 数值增强 + 数据类型Int

    }

    /// <summary>
    /// 魔法卡，部分实现
    /// </summary>
    public class SpellCard : Card
    {
        public SpellType spellType;
        public ElementEnum elementType;


        public SpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, SpellType _spellType) : base(_id, _cardType, _cardName, _cardInfo)
        {
            spellType = _spellType;
        }
    }

    public class DamageSpellCard : SpellCard, IDamageable
    {
        public int BaseNumerical;
        public ElementEnum BaseElement;

        public SpellElementBuff elementBuff;
        public object elementBuffAppendix;

        public DamageSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, ElementEnum _atkElement, SpellType _spellType) : base(_id, _cardType, _cardName, _cardInfo, _spellType)
        {
            this.BaseNumerical = _atk;
            this.BaseElement = _atkElement;
        }

        public DamageStruct GetDamage()
        {
            // TODO: 根据角色实情获取增幅
            return new DamageStruct(BaseNumerical, BaseElement);
        }
    }

    public class CureSpellCard : SpellCard
    {
        public int BaseNumerical;

        public CureSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _hp, ElementEnum _element, SpellType _spellType) : base(_id, _cardType, _cardName, _cardInfo, _spellType)
        {
            this.BaseNumerical = _hp;

        }
    }

}
