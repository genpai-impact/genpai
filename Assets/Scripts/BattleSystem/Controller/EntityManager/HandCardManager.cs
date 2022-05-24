using System.Collections.Generic;
using UnityEngine;
using Messager;


namespace Genpai
{
    /// <summary>
    /// 手牌管理器
    /// TODO：将CardDeck部分操作转移实现
    /// </summary>
    public class HandCardManager
    {
        public readonly List<GameObject> HandCards = new List<GameObject>();
        public BattleSite PlayerSite;

        public static GameObject GetCardPrefab(cfg.card.CardType cardType)
        {
            return cardType switch
            {
                cfg.card.CardType.Monster => PrefabsLoader.Instance.cardPrefab,
                cfg.card.CardType.Spell => PrefabsLoader.Instance.spellPrefab,
                _ => throw new System.Exception("不存在的卡牌类型")
            };
        }

        /// <summary>
        /// 卡牌实体化
        /// </summary>
        public GameObject Instantiate(Card cardDrawn, BattleSite site)
        {
            PlayerSite = site;
            GameObject cardPrefab = GetCardPrefab(cardDrawn.CardType);
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

            GameObject newCard = Object.Instantiate(cardPrefab, cardPool.transform);

            newCard.GetComponent<CardDisplay>().Card = cardDrawn;
            newCard.transform.position = cardHeap.transform.position;

            newCard.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
            newCard.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            //注册入卡牌管理器
            HandCards.Add(newCard);
            return newCard;
        }

        public void MoveToPool(GameObject newCard)
        {
            MoveToLast(newCard);
        }

        /// <summary>
        /// 牌库飞入动画
        /// </summary>
        public void MoveToLast(GameObject gameObject)
        {
            CardAniController cardAniController = gameObject.GetComponent<CardAniController>();
            cardAniController.MoveTo(new MoveToData(gameObject, new Vector3(-550 + HandCards.Count * 120, -100, 0)));
            AudioManager.Instance.PlayerEffect("Battle_DrawCard");
        }

        /// <summary>
        /// 移除召唤卡牌，剩余卡牌前移一位
        /// </summary>
        public void HandCardSort(GameObject card)
        {

            for (var i = 0; i < HandCards.Count; i++)
            {
                if (HandCards[i] != card)
                {
                    continue;
                }
                HandCards.RemoveAt(i);
                for (var j = i; j < HandCards.Count; j++)
                {
                    MoveToFormer(HandCards[j], j);
                }
                break;
            }
        }

        public static void MoveToFormer(GameObject gameObject, int handCardsNum)
        {
            CardAniController cardAniController = gameObject.GetComponent<CardAniController>();
            cardAniController.MoveTo(new MoveToData(gameObject, new Vector3(-430 + handCardsNum * 120, -100, 0)));
        }
    }
}