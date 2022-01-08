using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// ����չ������
    /// </summary>
    public enum CardType
    {
        chara,      // ��ɫ��
        monster,    // ���￨
        spell       // ħ����
    }

    /// <summary>
    /// ����������Ϣ(��������漰������Ϣ)
    /// </summary>
    public class Card
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
    }
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


    public class SpellCard : Card
    {
        public SpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo) : base(_id, _cardType, _cardName, _cardInfo)
        {

        }
    }
}


