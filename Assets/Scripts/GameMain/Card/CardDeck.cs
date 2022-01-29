
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

        /// <summary>
        /// 尽最大可能发/摸牌（！不是抽牌） charaN:发放角色数 cardN：发放卡牌数
        /// 返回值：1 牌库够；-1 牌库不够；0：手牌溢出，依然发牌，需执行CheckAndCullCard()剔除操作
        /// </summary>
        public int HandOutCard(int cardN)
        {
            int ret = 1;
            if (cardN > CardLibrary.Count)
            {
                cardN = CardLibrary.Count;
                ret = -1;
            }
            if (HandCardList.Count + cardN > S_HandCardLimit)
            {
                ret = 0;
            }
            for (int i = 0; i < cardN; i++)
            {
                DrawCard();
            }
            return ret;
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

            foreach (Card card in charaCard)
            {
                CharaLibrary.AddLast(card);
            }
            foreach (Card card in monsterCard)
            {
                CardLibrary.AddLast(card);
            }
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
        public static void RadomSort(ref List<Card> arr)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                var index = new System.Random().Next(i, arr.Count);
                var tmp = arr[i];
                var ran = arr[index];
                arr[i] = ran;
                arr[index] = tmp;
            }

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

            // >>>TODO: 以下部分转移至HandCardManager

            if (HandCardList.Count >= S_HandCardLimit)
            {
                return;
            }
            HandCardList.AddLast(DrawedCard);


            // 生成对应卡牌塞进界面
            // TODO：更换Prefabs设置入口
            GameObject newCard = GameObject.Instantiate(processtest.Instance.cardPrefab, processtest.Instance.cardPool.transform);


            //卡牌初始化
            newCard.GetComponent<CardDisplay>().card = DrawedCard;
            newCard.AddComponent<CardAniController>();
            newCard.GetComponent<CardPlayerController>().player = GameContext.Player1;

            newCard.transform.position = processtest.Instance.cardHeap.transform.position;
            newCard.transform.localScale = new Vector3(0.5f, 0.5f, 1);

            //注册入卡牌管理器
            HandCardManager.Instance.handCards.Add(newCard);

            //平滑移动至排尾
            MoveToLast(newCard);
        }

        /// <summary>
        /// 牌库飞入动画
        /// </summary>
        public void MoveToLast(GameObject gameObject)
        {
            Vector3 target = new Vector3(-550 + HandCardManager.Instance.handCards.Count * 120, 0, 0);
            MoveToData moveMessage = new MoveToData(gameObject, target);

            /// <summary>
            /// 发送消息：令卡牌移动至
            /// 消息类型：CardEvent.MoveTo
            /// 消息包：moveMessage
            /// </summary>
            MessageManager.Instance.Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, moveMessage);
        }



        public void DrawHero()
        {
            // 应该不会出现角色库无角色时抽取角色的情况
            if (CharaLibrary.Count == 0)
            {
                return;
            }

            Card DrawedChara = CharaLibrary.First.Value;
            CharaLibrary.Remove(DrawedChara);

            Unit temp = new Chara(DrawedChara as UnitCard);
            // TODO：将角色塞入玩家列表
            GameObject newCard = GameObject.Instantiate(processtest.Instance.cardPrefab, processtest.Instance.cardPool.transform);

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

