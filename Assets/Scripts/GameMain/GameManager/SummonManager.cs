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
    public class SummonManager : MonoSingleton<SummonManager>
    {


        private GameObject waitingUnit;
        private List<bool> waitingBucket;
        public bool summonWaiting;
        public GenpaiPlayer waitingPlayer;

        
        /// <summary>
        /// 校验&执行召唤请求
        /// </summary>
        /// <param name="_unitCard">召唤媒介单位牌</param>
        public void SummonRequest(GameObject _unitCard)
        {
            //Debug.LogWarning("SummonRequest");
            GenpaiPlayer tempPlayer = _unitCard.GetComponent<CardOnHand>().player;
            // 调用单例战场管理器查询玩家场地空闲
            bool bucketFree = false;
            List<bool> summonHoldList = BattleFieldManager.Instance.CheckSummonFree(tempPlayer, ref bucketFree);

            /*string a = "-------------";
            for (int i=0;i<summonHoldList.Count; i++) {
                a += summonHoldList[i] ? "1" : "0";
            }
            Debug.Log(a);*/

           
            if (bucketFree)
            {
                waitingPlayer = tempPlayer;
                //waitingUnit = _unitCard;
                //waitingBucket = summonHoldList;
                summonWaiting = true;
                // 场地高亮提示信息（由场地UI管理器受理）
                //Dispatch(MessageArea.UI, 0, waitingBucket);
                //Debug.LogWarning("sendmessage");
                MessageManager.Instance.Dispatch<List<bool>>(MessageArea.Summon, "SummonRequest", summonHoldList);
            }


        }


        /// <summary>
        /// 确认召唤请求
        /// </summary>
        /// <param name="_targetBucket">召唤目标格子</param>
        public void SummonConfirm(GameObject _targetBucket)
        {
            // 还需追加召唤次数检验（战斗管理器）
            if (true && summonWaiting)
            {
                // 解除场地高亮（由场地UI管理器受理）
                // Dispatch(MessageArea.UI, 0, 0);
                Summon(waitingPlayer, waitingUnit, _targetBucket);
            }
        }

        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="_player">进行召唤角色</param>
        /// <param name="_unitCard">召唤参考单位卡（可修改为依ID读数据库）</param>
        /// <param name="_targetBucket">召唤目标格子</param>
        public void Summon(GenpaiPlayer _player, GameObject _unitCard, GameObject _targetBucket)
        {
            //Debug.LogError("summon!!!!!!!!!!!!!!!!!!!!!!!!");
            UnitCard summonCard = _unitCard.GetComponent<CardDisplay>().card as UnitCard;
            string path = "UnitModel\\ModelImage\\Materials\\" + _unitCard.GetComponent<CardDisplay>().card.cardName;
            Material material = Resources.Load(path) as Material;
            Unit unit = new Unit(summonCard);
             _targetBucket.transform.Find("unit").GetComponent<Renderer>().material= material;

            BattleFieldManager.Instance.SetBucketCarryFlag(_targetBucket.GetComponent<BucketDisplay>().bucket.serial);

            _unitCard.GetComponent<CardControler>().RemoveSubscribe();
            Destroy(_unitCard);

            // 由场地管理器接管召唤过程（生成obj并更新信息）
            // BattleFieldManager.Instance.
        }

        public void Execute(string eventCode, object message)
        {
            // 处理召唤信息
        }


        public void Subscribe()
        {
            

            MessageManager.Instance.GetManager(MessageArea.Summon).Subscribe<GameObject>(MessageEvent.SummonEvent.SummonRequest, SummonRequest);

            // 向模块管理器追加订阅
        }

        public void Dispatch(string eventCode, object message)
        {
            
        }

        public void SummonEnd(MessageArea areaCode, string eventCode, bool message) {
            //Debug.LogWarning("SummonEnd");
            MessageManager.Instance.Dispatch<bool>(MessageArea.Summon, "SummonEnd", message);
        }



        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch  (eventCode){
                case "SummonRequest":
                    SummonRequest((GameObject)message);
                    break;
                case "SummonEnd":
                    SummonEnd(areaCode, eventCode, (bool) message);
                    break;
                case "Summon":
                    SummonData temp = (SummonData)message;
                    Summon(temp.player,temp.unitCard,temp.targetBucket);
                    break;
            }
            
        }
    }
}