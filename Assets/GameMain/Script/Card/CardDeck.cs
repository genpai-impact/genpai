
using System;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 卡组
    /// </summary>
    public class CardDeck
    {
        /// <summary>
        /// 手牌上限，不做成常量是因为未来可能有干涉手牌上限的卡
        /// </summary>
        public static int S_HandCardLimit = 10;

        /// <summary>
        /// 带上场的卡组
        /// </summary>
        LinkedList<BaseCard> CardLibrary = new LinkedList<BaseCard>();
        /// <summary>
        /// 手牌
        /// </summary>
        LinkedList<BaseCard> HandCardList = new LinkedList<BaseCard>();
        /// <summary>
        /// 已用卡组
        /// </summary>
        LinkedList<BaseCard> UsedList = new LinkedList<BaseCard>();
        /// <summary>
        /// 带上场的英雄
        /// </summary>
        LinkedList<BaseHero> HeroLibrary = new LinkedList<BaseHero>();
        public void Init()
        {

        }

        /// <summary>
        /// 使用卡
        /// </summary>
        public void UseCard(BaseCard card, BattlegroundBucket target)
        {
            if (CardLibrary.Find(card) == null)
            {
                throw new Exception("不能使用不存在的卡");
            }
            CardLibrary.Remove(card);
            card.Use(target);
            UsedList.AddLast(card);
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
            BaseCard DrawedCard = CardLibrary.First.Value;
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
            foreach (BaseHero hero in HeroLibrary)
            {
                if (HeroStatusEnum.OnCardDeck.Equals(hero.GetHeroStatus()))
                {
                    hero.SetHeroStatus(HeroStatusEnum.OnHand);
                    return;
                }
            }
        }
    }
}
