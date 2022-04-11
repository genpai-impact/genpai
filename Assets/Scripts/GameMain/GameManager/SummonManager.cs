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
            ClickManager.Instance.CancelAllClickAction();

            BattleSite tempPlayer = _unitCard.GetComponent<CardPlayerController>().playerSite;
            GenpaiPlayer genpaiPlayer = GameContext.Instance.GetPlayerBySite(waitingPlayer);

            if (genpaiPlayer.CurrentRoundMonsterCount >= GameContext.MissionConfig.RoundMonsterCount)
            {
                // 本回合已经召唤过了
                return;
            }
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
            if (summonWaiting && targetBucket.GetComponent<BucketPlayerController>().summoning)
            {
                summonWaiting = false;
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
                Summon(waitingUnit, targetBucket, waitingPlayer == BattleSite.P2);
                GenpaiPlayer genpaiPlayer = GameContext.Instance.GetPlayerBySite(waitingPlayer);
                genpaiPlayer.CurrentRoundMonsterCount++;
            }
        }

        public void SummonCancel()
        {
            summonWaiting = false;
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
                unit.transform.Find("UI/UnitUI/HPCanvas/HPText").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/AtkCanvas/AtkText ").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/AtkCanvas/AttackEle").Rotate(new Vector3(0, 180, 0));
            }

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(waitingPlayer, _targetBucket.GetComponent<BucketEntity>());

            BucketEntityManager.Instance.SetBucketCarryFlag(_targetBucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());


            // TODO: 明确音效指定
            AudioManager.Instance.PlayerEffect();

            int serial = _targetBucket.GetComponent<BucketEntity>().serial;
            Bucket newBucket = BattleFieldManager.Instance.GetBucketBySerial(serial);

            Unit newUnit = new Unit(summonCard, newBucket);
            Debug.Log(newUnit.unitName);
            unit.GetComponent<UnitDisplay>().FreshUnitUI(newUnit.GetView());

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
            GenpaiPlayer player = GameContext.Instance.GetPlayerBySite(waitingPlayer);
            for (int i = 0; i < player.HandCardManager.handCards.Count; i++)
            {
                if (player.HandCardManager.handCards[i] != _unitCard)
                {
                    continue;
                }
                player.HandCardManager.handCards.RemoveAt(i);
                for (int j = i; j < player.HandCardManager.handCards.Count; j++)
                {
                    MoveToFormer(player.HandCardManager.handCards[j], j);
                }
                break;
            }
        }

        public void MoveToFormer(GameObject gameObject, int handCardsNum)
        {
            CardAniController cardAniController = gameObject.GetComponent<CardAniController>();
            cardAniController.MoveTo(new MoveToData(gameObject, new Vector3(-430 + handCardsNum * 120, -100, 0)));
        }
    }
}