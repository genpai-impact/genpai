using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;


namespace Genpai
{
    /// <summary>
    /// 手牌管理器
    /// TODO：将CardDeck部分操作转移实现
    /// </summary>
    public class HandCardManager
    {
        //public List<GameObject> handCharas = new List<GameObject>();

        public List<GameObject> handCards = new List<GameObject>();

        public GameObject GetCardPrefeb(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.monsterCard:
                    return PrefabsLoader.Instance.cardPrefab;
                case CardType.spellCard:
                    return PrefabsLoader.Instance.spellPrefab;
                default:
                    throw new System.Exception("不存在的卡牌类型");
            }
        }


        /// <summary>
        /// 卡牌实体化
        /// </summary>
        public GameObject Instantiate(Card drawedCard, BattleSite site)
        {

            GameObject cardPrefab = GetCardPrefeb(drawedCard.cardType);
            GameObject cardPool;
            GameObject cardHeap;
            if (site == BattleSite.P1)
            {
                cardPool = PrefabsLoader.Instance.cardPool;
                cardHeap = PrefabsLoader.Instance.cardHeap;
            }
            else
            {
                cardPool = PrefabsLoader.Instance.card2Pool;
                cardHeap = PrefabsLoader.Instance.card2Heap;
            }
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool.transform);
            newCard.GetComponent<CardDisplay>().card = drawedCard;
            newCard.transform.position = cardHeap.transform.position;
            newCard.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
            newCard.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            //注册入卡牌管理器
            handCards.Add(newCard);
            return newCard;
        }

        public void MoveToPool(GameObject newCard)
        {
            MoveToLast(newCard);

            //魔法卡没有绑CardPlayerController，用不了这个
            //这里的判断似乎没什么用，注释了
            //if (newCard.GetComponent<CardPlayerController>().playerSite == BattleSite.P1)
            //{
            //    MoveToLast(newCard);
            //}
            //else if (newCard.GetComponent<CardPlayerController>().playerSite == BattleSite.P2)
            //{
            //
            //    MoveToLast(newCard);
            //}

        }

        /// <summary>
        /// 牌库飞入动画
        /// </summary>
        public void MoveToLast(GameObject gameObject)
        {
            Vector3 target = new Vector3(-550 + handCards.Count * 120, -100, 0);
            MoveToData moveMessage = new MoveToData(gameObject, target);

            /// <summary>
            /// 发送消息：令卡牌移动至
            /// 消息类型：CardEvent.MoveTo
            /// 消息包：moveMessage
            /// </summary>
            MessageManager.Instance.Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, moveMessage);
        }
    }




}