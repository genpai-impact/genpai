using System;

namespace DataScripts.Card
{

    /// <summary>
    /// 基础卡牌信息(卡牌类仅涉及保存信息和显示)
    /// </summary>
    public class Card : ICloneable
    {
        public readonly int CardID;
        public readonly cfg.card.CardType CardType;
        public readonly string CardName;
        public readonly string[] CardInfo;

        public Card() { }

        public Card(int id, cfg.card.CardType cardType, string cardName, string[] cardInfo)
        {
            CardID = id;
            CardType = cardType;
            CardName = cardName;
            CardInfo = cardInfo;
        }

        public object Clone()  // 此方法目前只看到给CardDeck用
        {
            return MemberwiseClone();
        }
    }
}


