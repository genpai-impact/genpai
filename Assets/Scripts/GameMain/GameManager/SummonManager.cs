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
            // 获取卡牌数据
            UnitCard summonCard = _unitCard.GetComponent<CardDisplay>().card as UnitCard;
            // 获取模型（简易）
            //string path = "UnitModel\\ModelImage\\Materials\\" + _unitCard.GetComponent<CardDisplay>().card.cardName;
            string path = "UnitModel\\ModelImage\\" + _unitCard.GetComponent<CardDisplay>().card.cardName;
            //Material material = Resources.Load(path) as Material;

            // TODO：生成实际UnitEntity
            Transform obj = _targetBucket.transform.Find("Unit");
            obj.gameObject.SetActive(true);
            obj.GetComponent<UnitEntity>().Init(summonCard);
            obj.GetComponent<UnitDisplay>().Init();

            //_targetBucket.transform.Find("unit").GetComponent<Renderer>().material = material;
            Unit unit = new Unit(summonCard);


            BattleFieldManager.Instance.SetBucketCarryFlag(_targetBucket.GetComponent<BucketUIController>().bucket.serial);


            // 析构卡牌（暂时用取消激活实现）
            //_unitCard.GetComponent<CardControler>().RemoveSubscribe();
            _unitCard.SetActive(false);

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