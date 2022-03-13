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


        public SpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo,
            SpellType _spellType, ElementEnum _elementType)
            : base(_id, _cardType, _cardName, _cardInfo)
        {
            spellType = _spellType;
            elementType = _elementType;
        }
    }

    public class DamageSpellCard : SpellCard
    {
        public int BaseNumerical;

        public SpellElementBuff elementBuff;
        public object elementBuffAppendix;

        public DamageSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo,
            SpellType _spellType, ElementEnum _elementType, int _atk)
            : base(_id, _cardType, _cardName, _cardInfo, _spellType, _elementType)
        {
            this.BaseNumerical = _atk;
        }

        public DamageStruct GetDamage(ElementEnum _elementEnum)
        {
            // TODO: 根据角色实情获取增幅
            if (_elementEnum == elementType)
            {
                return new DamageStruct(BaseNumerical, elementType);
            }
            else
            {
                return new DamageStruct(BaseNumerical, ElementEnum.None);
            }

        }
    }

    public class CureSpellCard : SpellCard
    {
        public int BaseNumerical;

        public CureSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo,
             SpellType _spellType, ElementEnum _elementType, int _hp)
            : base(_id, _cardType, _cardName, _cardInfo, _spellType, _elementType)
        {
            this.BaseNumerical = _hp;

        }
    }

    public class BuffSpellCard : SpellCard
    {
        public int buffNum;
        public BuffEnum buffName;

        public BuffSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo,
            SpellType _spellType, ElementEnum _elementType, int _buffNum, BuffEnum _buffName)
            : base(_id, _cardType, _cardName, _cardInfo, _spellType, _elementType)
        {
            this.buffNum = _buffNum;
            this.buffName = _buffName;
        }
    }
}
