﻿using System.Collections;
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
        public void SummonConfirm()
        {
            GameObject targetBucket = SummonManager.Instance.waitingBucket;
            // 还需追加召唤次数检验（战斗管理器）
            if (summonWaiting && targetBucket.GetComponent<BucketPlayerController>().summoning)
            {
                summonWaiting = false;
                // 关闭高亮
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
                Summon(waitingUnit, targetBucket, waitingPlayer == BattleSite.P2);
            }
        }
        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="_player">进行召唤角色</param>
        /// <param name="_unitCard">召唤参考单位卡（可修改为依ID读数据库）</param>
        /// <param name="_targetBucket">召唤目标格子</param>
        /// <param name="IsP2">是否为P2（控制朝向）</param>
        public void Summon(UnitCard summonCard, GameObject _targetBucket, bool IsP2)
        {
            // 生成实际UnitEntity
            Transform UnitSeats = _targetBucket.transform.Find("Unit");
            GameObject unit = GameObject.Instantiate(PrefabsLoader.Instance.unitPrefab, UnitSeats.transform);

            if (IsP2 == true)
            {
                unit.transform.Rotate(new Vector3(0, 180, 0));

                unit.transform.Find("UI/UnitUI/HPCanvas/AttachEle").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/HPCanvas/Image").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/HPCanvas/HPText").Rotate(new Vector3(0, 180, 0));
                
                unit.transform.Find("UI/UnitUI/AtkCanvas/AttackEle").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/AtkCanvas/Image").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/AtkCanvas/AtkText ").Rotate(new Vector3(0, 180, 0));
            }

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(summonCard, waitingPlayer, _targetBucket.GetComponent<BucketEntity>());
            unit.GetComponent<UnitDisplay>().Init();


            BattleFieldManager.Instance.SetBucketCarryFlag(_targetBucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());

        }


        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="_player">进行召唤角色</param>
        /// <param name="_unitCard">召唤参考单位卡（可修改为依ID读数据库）</param>
        /// <param name="_targetBucket">召唤目标格子</param>
        /// <param name="IsP2">是否为P2（控制朝向）</param>
        public void Summon(GameObject _unitCard, GameObject _targetBucket, bool IsP2)
        {

            // 获取卡牌数据
            UnitCard summonCard = _unitCard.GetComponent<CardDisplay>().card as UnitCard;

            Summon(summonCard, _targetBucket, IsP2);

            // 析构卡牌（暂时用取消激活实现）
            //_unitCard.GetComponent<CardControler>().RemoveSubscribe();
            _unitCard.SetActive(false);

            //召唤成功，目标卡牌从手牌移除,整理剩余手牌
            HandCardsort(_unitCard);



        }

        public void MagicSummon(GameObject _spellCard)
        {
            waitingPlayer = _spellCard.GetComponent<SpellPlayerController>().playerSite;
            _spellCard.SetActive(false);
            HandCardsort(_spellCard);

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

        public void MoveToFormer(GameObject gameObject, int handCardsNum)
        {
            CardAniController cardAniController = gameObject.GetComponent<CardAniController>();
            cardAniController.MoveTo(new MoveToData(gameObject, new Vector3(-430 + handCardsNum * 120, -100, 0)));
        }


        public void Subscribe()
        {
            //订阅魔法卡召唤
            MessageManager.Instance.GetManager(MessageArea.Summon)
                .Subscribe<GameObject>(MessageEvent.SummonEvent.MagicSummon, MagicSummon);

        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {

        }
    }
}