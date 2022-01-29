using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 待拓展枚举卡牌种类
    /// </summary>
    public enum CardType
    {
        charaCard,      // 角色卡
        monsterCard,    // 怪物卡
        spellCard       // 魔法卡
    }

    /// <summary>
    /// 基础卡牌信息(卡牌类仅涉及保存信息和显示)
    /// </summary>
    public class Card : ICloneable
    {
        public int cardID;
        public CardType cardType;
        public string cardName;
        public string[] cardInfo;


        public Card(int _id, string _cardType, string _cardName, string[] _cardInfo)
        {
            this.cardID = _id;
            this.cardType = (CardType)System.Enum.Parse(typeof(CardType), _cardType);
            this.cardName = _cardName;
            this.cardInfo = _cardInfo;
        }

       

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class UnitCard : Card
    {
        public int atk;
        public int hp;
        public ElementEnum atkElement;
        public ElementEnum selfElement;

        public UnitCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, int _hp, ElementEnum _atkElement, ElementEnum _selfElement) : base(_id, _cardType, _cardName, _cardInfo)
        {
            this.atk = _atk;
            this.hp = _hp;
            this.atkElement = _atkElement;
            this.selfElement = _selfElement;
        }

       

    }

    /// <summary>
    /// 魔法卡，未实现
    /// </summary>
    public class SpellCard : Card
    {
        public int atk;
        public ElementEnum atkElement;

        public SpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, ElementEnum _atkElement) : base(_id, _cardType, _cardName, _cardInfo)
        {
            this.atk = _atk;
            this.atkElement = _atkElement;
        }

        
    }
}


