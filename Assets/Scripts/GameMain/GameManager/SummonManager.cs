using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class SummonManager : MonoSingleton<SummonManager>, IMessageHandler
    {


        private GameObject waitingUnit;
        private List<bool> waitingBucket;
        public bool summonWaiting;
        public PlayerID waitingPlayer;


        /// <summary>
        /// 校验&执行召唤请求
        /// </summary>
        /// <param name="_unitCard">召唤媒介单位牌</param>
        public void SummonRequest(GameObject _unitCard)
        {
            PlayerID tempPlayer = _unitCard.GetComponent<CardOnHand>().player;
            // 调用单例战场管理器查询玩家场地空闲
            (bool bucketFree, List<bool> summonHoldList) = BattleFieldManager.Instance.CheckSummonFree(tempPlayer);

            if (bucketFree)
            {
                waitingPlayer = tempPlayer;
                waitingUnit = _unitCard;
                waitingBucket = summonHoldList;
                summonWaiting = true;
                // 场地高亮提示信息（由场地UI管理器受理）
                Dispatch(MessageArea.UI, 0, waitingBucket);
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
                Dispatch(MessageArea.UI, 0, 0);
                Summon(waitingPlayer, waitingUnit, _targetBucket);
            }
        }

        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="_player">进行召唤角色</param>
        /// <param name="_unitCard">召唤参考单位卡（可修改为依ID读数据库）</param>
        /// <param name="_targetBucket">召唤目标格子</param>
        public void Summon(PlayerID _player, GameObject _unitCard, GameObject _targetBucket)
        {
            UnitCard summonCard = _unitCard.GetComponent<CardDisplay>().card as UnitCard;
            Unit unit = new Unit(summonCard, _player);
            // 由场地管理器接管召唤过程（生成obj并更新信息）
            // BattleFieldManager.Instance.
        }

        public void Execute(int eventCode, object message)
        {
            // 处理召唤信息
        }


        public void Subscribe()
        {
            // 向模块管理器追加订阅
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}