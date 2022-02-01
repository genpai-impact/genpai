
using System;
using System.Collections.Generic;
using UnityEngine;
using Messager;
namespace Genpai
{
    /// <summary>
    /// 牌库，实现单场对局中特定玩家的手牌管理
    /// </summary>
    public class CardDeck
    {
        public GenpaiPlayer owner;

        public static int S_HandCardLimit = 10; // 手牌上限

        /// <summary>
        /// 起始手牌数量
        /// </summary>
        public const int _startCardCount = 4;
        /// <summary>
        /// 起手英雄数量
        /// </summary>
        public const int _startHeroCount = 2;
        /// <summary>
        /// 每回合抽卡数量
        /// </summary>
        public const int _roundCardCount = 1;

        /// <summary>
        /// 牌库
        /// </summary>
        public LinkedList<Card> CardLibrary = new LinkedList<Card>();

        /// <summary>
        /// 手牌
        /// </summary>
        public LinkedList<Card> HandCardList = new LinkedList<Card>();

        /// <summary>
        /// 带上场的角色
        /// 角色不参与发牌流程，仅于牌库暂存
        /// </summary>
        public LinkedList<Card> CharaLibrary = new LinkedList<Card>();



        /// <summary>
        /// 由选出的卡中检查并剔除
        /// </summary>
        public void CheckAndCullCard()
        {

        }

        

        public void Init(List<int> cardIdList)
        {
            List<Card> selectedCard = CardLoader.Instance.GetCardByIds(cardIdList);
            List<Card> charaCard = new List<Card>();
            List<Card> monsterCard = new List<Card>();

            foreach (Card card in selectedCard)
            {
                if (!(card is UnitCard))
                {
                    continue;
                }
                if (card.cardType is CardType.charaCard)
                {
                    charaCard.Add((Card)card.Clone());
                }
                else
                {
                    monsterCard.Add((Card)card.Clone());
                }
            }

            RadomSort(ref charaCard);
            RadomSort(ref monsterCard);

            for (int i = 0; i < charaCard.Count; i++) {
                CharaLibrary.AddLast(charaCard[i]);
            }
            for (int i = 0; i < monsterCard.Count; i++)
            {
                CardLibrary.AddLast(monsterCard[i]);
            }

            /*foreach (Card card in charaCard)
            {
                CharaLibrary.AddLast(card);
            }
            foreach (Card card in monsterCard)
            {
                CardLibrary.AddLast(card);
            }*/
        }

        /// <summary>
        /// 根据输入玩家卡组构造牌库
        /// </summary>
        public CardDeck()
        {
        }

        /// <summary>
        /// 洗牌算法
        /// </summary>
        /// <param name="arr"></param>
        public void RadomSort(ref List<Card> arr)
        {
            Debug.LogWarning("sort");
            for (int i = 0; i < arr.Count; i++)
            {
                
                var index = new System.Random((int)DateTime.Now.Ticks).Next(i, arr.Count);
                var tmp = arr[i];
                var ran = arr[index];
                arr[i] = ran;
                arr[index] = tmp;
            }

        }

        /// <summary>
        /// 抽牌
        /// </summary>
        public Card DrawCard()
        {
            // 无牌情况
            if (CardLibrary.Count == 0)
            {
                return null;
            }
            Card DrawedCard = CardLibrary.First.Value;
            CardLibrary.Remove(DrawedCard);

            // >>>TODO: 以下部分转移至HandCardManager

            if (HandCardList.Count >= S_HandCardLimit)
            {
                return null;
            }
            HandCardList.AddLast(DrawedCard);
            return DrawedCard;

        }

        



        public Card DrawChara()
        {
            // 应该不会出现角色库无角色时抽取角色的情况
            if (CharaLibrary.Count == 0)
            {
                return null;
            }

            Card DrawedChara = CharaLibrary.First.Value;
            CharaLibrary.Remove(DrawedChara);

            return DrawedChara;

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

