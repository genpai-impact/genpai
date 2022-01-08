
using System;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 卡组
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
        // LinkedList<Hero> HeroLibrary = new LinkedList<Hero>();
        public void Init()
        {

        }

    }
}

