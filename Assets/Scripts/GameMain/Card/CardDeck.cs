
using System;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 卡组
    /// 弃牌需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61cca0706eb9a5003fe3eb3a
    /// 
    /// </summary>
    public class CardDeck
    {
        public PlayerID owner;

        public static int S_HandCardLimit = 10;

        /// <summary>
        /// 带上场的卡组
        /// </summary>
        LinkedList<Card> CardLibrary = new LinkedList<Card>();
        /// <summary>
        /// 手牌
        /// </summary>
        LinkedList<Card> HandCardList = new LinkedList<Card>();
        /// <summary>
        /// 已用卡组
        /// </summary>
        LinkedList<Card> UsedList = new LinkedList<Card>();
        /// <summary>
        /// 带上场的英雄
        /// </summary>
        LinkedList<Card> HeroLibrary = new LinkedList<Card>();
        public void Init()
        {

        }

        /// <summary>
        /// 抽牌
        /// </summary>
        public void DrawCard()
        {
            // 无牌情况
            if (CardLibrary.Count == 0)
            {
                return;
            }
            Card DrawedCard = CardLibrary.First.Value;
            CardLibrary.Remove(DrawedCard);
            if (HandCardList.Count >= S_HandCardLimit)
            {
                return;
            }
            HandCardList.AddLast(DrawedCard);
        }
        /// <summary>
        /// 抽英雄
        /// </summary>
        public void DrawHero()
        {
            foreach (Card hero in HeroLibrary)
            {

            }
        }

    }
}

