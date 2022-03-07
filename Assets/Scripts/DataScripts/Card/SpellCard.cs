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
        Control,    //控制
        Draw,       //抽牌
        Upgrade,    //增益
        Cure,       //治疗
        Protect,    //保护
        Weather,    //天气
        Special     //同时具有以上多种
    }
    /// <summary>
    /// 魔法卡，部分实现
    /// </summary>
    public class SpellCard : Card
    {
        SpellType spellType;

        public SpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo,SpellType _spellType) : base(_id, _cardType, _cardName, _cardInfo)
        {
            spellType = _spellType;
        }
    }

    public class DamageSpellCard : SpellCard
    {
        public int atk;
        public ElementEnum atkElement;

        public DamageSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, ElementEnum _atkElement, SpellType _spellType) : base(_id, _cardType, _cardName, _cardInfo,_spellType)
        {
            this.atk = _atk;
            this.atkElement = _atkElement;
        }

    }

    public class CureSpellCard : SpellCard
    {
        public int HP;
        public ElementEnum element;
        public CureSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _hp, ElementEnum _element, SpellType _spellType) : base(_id, _cardType, _cardName, _cardInfo, _spellType)
        {
            this.HP= _hp;
            this.element = _element;
        }
    }

}
