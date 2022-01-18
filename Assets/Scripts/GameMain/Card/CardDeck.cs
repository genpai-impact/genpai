﻿
using System;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 卡组，实现单场对局中特定玩家的手牌管理
    /// </summary>
    public class CardDeck
    {
        public PlayerID owner;

        public static int S_HandCardLimit = 10; // 手牌上限

        /// <summary>
        /// 带上场的卡组
        /// </summary>
        LinkedList<Card> CardLibrary = new LinkedList<Card>();

        /// <summary>
        /// 手牌
        /// </summary>
        LinkedList<Card> HandCardList = new LinkedList<Card>();

        /// <summary>
        /// 已用卡牌
        /// </summary>
        LinkedList<Card> UsedList = new LinkedList<Card>();

        /// <summary>
        /// 带上场的角色
        /// </summary>
        LinkedList<Card> CharaLibrary = new LinkedList<Card>();

        /// <summary>
        /// 通过玩家自定卡组构造，待实现
        /// </summary>
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
        /// 抽角色
        /// </summary>
        public void DrawHero()
        {
            foreach (Card chara in CharaLibrary)
            {

            }
        }

        ///<summary>
        ///获取余牌数量
        /// </summary>
        public int GetRemainCard()
        {
            return CardLibrary.Count;
        }
    }
}

