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
        public ElementEnum elementType;
        public SpellType spellType;
        public object MagicTypeAppendix;
        public TargetType targetType;
        public TargetArea targetArea;
        public int BaseNumerical;
        public SpellElementBuff spellElementBuff;
        public object ElementBuffAppendix;


        public SpellCard(int _id, string _cardType, SpellCardData data)
            : base(_id, _cardType, data.magicName, data.CardInfo.Split('\n'))
        {
            elementType = data.elementType;
            spellType = data.magicType;
            MagicTypeAppendix = data.MagicTypeAppendix;
            targetType = data.targetType;
            targetArea = data.targetArea;
            BaseNumerical = data.BaseNumerical;
            spellElementBuff = data.elementBuff;
            ElementBuffAppendix = data.ElementBuffAppendix;
        }

    }

    public class DamageSpellCard : SpellCard
    {

        DamageStruct damageStruct;
        public DamageSpellCard(int _id, string _cardType, SpellCardData data)
            : base(_id, _cardType, data)
        {
            damageStruct = new DamageStruct(data.BaseNumerical, ElementEnum.None);
        }

    }

    public class CureSpellCard : SpellCard
    {
        public CureSpellCard(int _id, string _cardType, SpellCardData data)
            : base(_id, _cardType, data)
        {
        }
    }

    public class BuffSpellCard : SpellCard
    {
        public BuffEnum buffName;

        public BuffSpellCard(int _id, string _cardType, SpellCardData data)
            : base(_id, _cardType, data)
        {
            this.buffName = (BuffEnum)System.Enum.Parse(typeof(BuffEnum), data.MagicTypeAppendix.ToString());
        }
    }

    public class DrawSpellCard : SpellCard
    {
        public DrawSpellCard(int _id, string _cardType, SpellCardData data)
            : base(_id, _cardType, data)
        {
        }
    }
}
