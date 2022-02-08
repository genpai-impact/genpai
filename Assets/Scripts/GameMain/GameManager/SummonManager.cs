using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
using System;

namespace Genpai
{
    /// <summary>
    /// 召唤管理器
    /// </summary>
    public class SummonManager : Singleton<SummonManager>
    {


        public GameObject waitingUnit;
        public GameObject waitingBucket;

        public bool summonWaiting;
        public BattleSite waitingPlayer;

        private SummonManager()
        {
            Subscribe();
        }
        public void Init()
        {

        }


        /// <summary>
        /// 校验&执行召唤请求
        /// </summary>
        /// <param name="_unitCard">召唤媒介单位牌</param>
        public void SummonRequest(GameObject _unitCard)
        {

            BattleSite tempPlayer = _unitCard.GetComponent<CardPlayerController>().playerSite;
            // 调用单例战场管理器查询玩家场地空闲
            bool bucketFree = false;

            List<bool> summonHoldList = BattleFieldManager.Instance.CheckSummonFree(tempPlayer, ref bucketFree);


            if (bucketFree)
            {

                waitingPlayer = tempPlayer;
                waitingUnit = _unitCard;

                summonWaiting = true;

                // 发送高亮提示消息
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.SummonHighLight, summonHoldList);

            }


        }


        /// <summary>
        /// 确认召唤请求
        /// </summary>
        /// <param name="_targetBucket">召唤目标格子</param>
        public void SummonConfirm(GameObject _targetBucket)
        {
            // Debug.Log("SM: Taking Confirm");

            // 还需追加召唤次数检验（战斗管理器）
            if (summonWaiting && _targetBucket.GetComponent<BucketReactionController>().summoning)
            {

                summonWaiting = false;
                Debug.Log("召唤：" + waitingUnit.GetComponent<CardDisplay>().card.cardName);

                // 关闭高亮
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);

                Summon(waitingUnit, _targetBucket);
            }
        }



        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="_player">进行召唤角色</param>
        /// <param name="_unitCard">召唤参考单位卡（可修改为依ID读数据库）</param>
        /// <param name="_targetBucket">召唤目标格子</param>
        public void Summon(GameObject _unitCard, GameObject _targetBucket)
        {
            Debug.Log("Summon");
            // 获取卡牌数据
            UnitCard summonCard = _unitCard.GetComponent<CardDisplay>().card as UnitCard;

            // 生成实际UnitEntity
            Transform UnitSeats = _targetBucket.transform.Find("Unit");
            GameObject unit = GameObject.Instantiate(PrefabsLoader.Instance.unitPrefab, UnitSeats.transform);

            if (waitingPlayer == BattleSite.P2)
            {
                unit.transform.Rotate(new Vector3(0, 180, 0));
            }

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(summonCard, waitingPlayer, _targetBucket.GetComponent<BucketEntity>());
            unit.GetComponent<UnitDisplay>().Init();


            BattleFieldManager.Instance.SetBucketCarryFlag(_targetBucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());


            // 析构卡牌（暂时用取消激活实现）
            //_unitCard.GetComponent<CardControler>().RemoveSubscribe();
            _unitCard.SetActive(false);

            //召唤成功，目标卡牌从手牌移除,整理剩余手牌
            HandCardsort(_unitCard);
            
            

        }

        /// <summary>
        /// 移除召唤卡牌，剩余卡牌前移一位
        /// </summary>
        public void HandCardsort(GameObject _unitCard)
        {
            if (waitingPlayer == BattleSite.P1)
            {
                Debug.Log(_unitCard);
                for (int i = 0; i < GameContext.Player1.HandCardManager.handCards.Count; i++)
                {
                    if (GameContext.Player1.HandCardManager.handCards[i] == _unitCard)
                    {

                        GameContext.Player1.HandCardManager.handCards.RemoveAt(i);
                        Debug.Log("移除第" + i + 1 + "张手牌" + _unitCard);
                        Debug.Log("P1剩余手牌数为：" + GameContext.Player1.HandCardManager.handCards.Count);
                        for (int j = i; j < GameContext.Player1.HandCardManager.handCards.Count; j++)
                        {
                            MoveToFormer(GameContext.Player1.HandCardManager.handCards[j], j);
                        }
                        break;
                    }
                }
            }
            else if (waitingPlayer == BattleSite.P2)
            {
                Debug.Log(_unitCard);
                for (int i = 0; i < GameContext.Player2.HandCardManager.handCards.Count; i++)
                {
                    if (GameContext.Player2.HandCardManager.handCards[i] == _unitCard)
                    {
                        GameContext.Player2.HandCardManager.handCards.RemoveAt(i);
                        Debug.Log("移除第" + i + 1 + "张手牌" + _unitCard);
                        Debug.Log("P2剩余手牌数为：" + GameContext.Player2.HandCardManager.handCards.Count);
                        for (int j = i; j < GameContext.Player2.HandCardManager.handCards.Count; j++)
                        {
                            MoveToFormer(GameContext.Player2.HandCardManager.handCards[j], j);
                        }
                        break;
                    }
                }
            }
        }

        public void MoveToFormer(GameObject gameObject,int handCardsNum)
        {
            Vector3 target = new Vector3(-430 + handCardsNum * 120, 0, 0);
            MoveToData moveMessage = new MoveToData(gameObject, target);

            /// <summary>
            /// 发送消息：令卡牌移动至前一位
            /// 消息类型：CardEvent.MoveTo
            /// 消息包：moveMessage
            /// </summary>
            MessageManager.Instance.Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, moveMessage);
        }


        public void Subscribe()
        {
            // 订阅召唤请求
            MessageManager.Instance.GetManager(MessageArea.Summon)
                .Subscribe<GameObject>(MessageEvent.SummonEvent.SummonRequest, SummonRequest);

            // 订阅召唤确认
            MessageManager.Instance.GetManager(MessageArea.Summon)
                .Subscribe<GameObject>(MessageEvent.SummonEvent.SummonConfirm, SummonConfirm);

        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {


        }

    }
}