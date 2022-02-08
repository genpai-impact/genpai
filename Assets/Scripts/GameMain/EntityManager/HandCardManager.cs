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
        public List<GameObject> handCharas = new List<GameObject>();

        public List<GameObject> handCards = new List<GameObject>();




        /// <summary>
        /// 卡牌实体化
        /// </summary>
        public GameObject Instantiate(Card DrawedCard, BattleSite site)
        {

            if (DrawedCard.cardType == CardType.charaCard)
            {
                Chara chara = new Chara(DrawedCard as UnitCard, 4);

                // 添加角色
                GameContext.Instance.GetPlayerBySite(site).CharaList.Add(chara);

                // 生成对应卡牌塞进界面
                // TODO：更换Prefabs设置入口
                GameObject newCard;
                if (site == BattleSite.P1)
                {
                    newCard = GameObject.Instantiate(PrefabsLoader.Instance.charaPrefab, PrefabsLoader.Instance.charaPool.transform);
                }
                else
                {
                    newCard = GameObject.Instantiate(PrefabsLoader.Instance.charaPrefab, PrefabsLoader.Instance.chara2Pool.transform);
                }

                //卡牌显示初始化
                newCard.GetComponent<CharaDisplay>().PlayerSite = site;
                newCard.GetComponent<CharaDisplay>().chara = chara;


                //newCard.GetComponent<CardPlayerController>().player = GameContext.Player1;


                // newCard.transform.position = processtest.Instance.charaPool.transform.position;

                if (site == BattleSite.P1)
                {
                    newCard.transform.position = PrefabsLoader.Instance.charaPool.transform.position;
                }
                else
                {
                    newCard.transform.position = PrefabsLoader.Instance.chara2Pool.transform.position;
                }


                newCard.transform.localScale = new Vector3(1, 1, 1);



                //注册入卡牌管理器
                handCharas.Add(newCard);

                return newCard;
            }
            else if (DrawedCard.cardType == CardType.monsterCard)
            {
                // 生成对应卡牌塞进界面
                // TODO：更换Prefabs设置入口
                GameObject newCard;
                if (site == BattleSite.P1)
                {
                    newCard = GameObject.Instantiate(PrefabsLoader.Instance.cardPrefab, PrefabsLoader.Instance.cardPool.transform);
                }
                else
                {
                    newCard = GameObject.Instantiate(PrefabsLoader.Instance.cardPrefab, PrefabsLoader.Instance.card2Pool.transform);
                }


                //卡牌初始化
                newCard.GetComponent<CardDisplay>().card = DrawedCard;
                //newCard.GetComponent<CardPlayerController>().player = GameContext.Player1;

                // newCard.transform.position = processtest.Instance.cardHeap.transform.position;
                if (site == BattleSite.P1)
                {
                    newCard.transform.position = PrefabsLoader.Instance.cardHeap.transform.position;
                }
                else
                {
                    newCard.transform.position = PrefabsLoader.Instance.card2Heap.transform.position;
                }

                newCard.transform.localScale = new Vector3(0.5f, 0.5f, 1);



                //注册入卡牌管理器
                handCards.Add(newCard);
                return newCard;

            }
            else
            {
                return null;
            }



        }

        public void MoveToPool(GameObject newCard)
        {

            if (newCard.GetComponent<CardPlayerController>().playerSite == BattleSite.P1)
            {
                MoveToLast(newCard);
            }
            else if (newCard.GetComponent<CardPlayerController>().playerSite == BattleSite.P2)
            {

                MoveToLast(newCard);
            }

        }

        /// <summary>
        /// 牌库飞入动画
        /// </summary>
        public void MoveToLast(GameObject gameObject)
        {
            Vector3 target = new Vector3(-550 + handCards.Count * 120, 0, 0);
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