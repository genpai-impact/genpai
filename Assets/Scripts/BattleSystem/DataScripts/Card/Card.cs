using System;

namespace Genpai
{

    /// <summary>
    /// 基础卡牌信息(卡牌类仅涉及保存信息和显示)
    /// </summary>
    public class Card : ICloneable
    {
        public int cardID;
        public CardType cardType;
        public string cardName;
        public string[] cardInfo;
        public int cardCnt;

        public Card()
        {

        }

        public Card(int _id, string _cardType, string _cardName, string[] _cardInfo)
        {
            this.cardID = _id;
            this.cardType = CardTypeFormString(_cardType);
            this.cardName = _cardName;
            this.cardInfo = _cardInfo;
        }


        public static CardType CardTypeFormString(string cardType)
        {
            return (CardType)System.Enum.Parse(typeof(CardType), cardType);
        }

        public object Clone()  // 此方法目前只看到给CardDeck用
        {
            return MemberwiseClone();
        }
    }
}


