using System.Collections.Generic;
using BattleSystem.Controller.Animator;
using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.EntityController;
using BattleSystem.Controller.UI;
using BattleSystem.Controller.Unit;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using DataScripts.Card;
using DataScripts.DataLoader;
using UnityEngine;
using Utils;
using Utils.Messager;
using Object = UnityEngine.Object;

namespace BattleSystem.Controller
{
    /// <summary>
    /// 召唤管理器
    /// </summary>
    public class SummonManager : Singleton<SummonManager>
    {
        public GameObject waitingUnit=null;
        public GameObject waitingBucket;

        public bool summonWaiting;
        public BattleSite waitingPlayer;

        private SummonManager()
        {
        }
        public void Init() { }
        
        // 只是为了在GameContextScript中进行新游戏的fresh的时候保持形式同一，没有特殊作用
        public void Fresh()
        {

        }

        /// <summary>
        /// 校验执行召唤请求
        /// </summary>
        /// <param name="unitCard">召唤媒介单位牌</param>
        public void SummonRequest(GameObject unitCard)
        {
            
            ClickManager.CancelAllClickAction();//!!!一定放在最前

            BattleSite tempPlayer = unitCard.GetComponent<CardPlayerController>().playerSite;
            GenpaiPlayer genpaiPlayer = GameContext.GetPlayerBySite(waitingPlayer);

            if (genpaiPlayer.CurrentRoundMonsterCount >= GameContext.MissionConfig.RoundMonsterCount)
            {
                // 本回合已经召唤过了
                return;
            }
            // 调用单例战场管理器查询玩家场地空闲
            var bucketFree = false;
            List<bool> summonHoldList = BattleFieldManager.Instance.CheckSummonFree(tempPlayer, ref bucketFree);
            if (bucketFree)
            {
                waitingPlayer = tempPlayer;
                waitingUnit = unitCard;
                summonWaiting = true;
                // 发送高亮提示消息
                MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.SummonHighLight, summonHoldList);
            }
        }

        /// <summary>
        /// 确认召唤请求
        /// </summary>
        public void SummonConfirm()
        {
            if (!summonWaiting || !waitingBucket.GetComponent<BucketPlayerController>().summoning) return;
            
            summonWaiting = false;
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            Summon(waitingUnit, waitingBucket, waitingPlayer == BattleSite.P2);
            GenpaiPlayer genpaiPlayer = GameContext.GetPlayerBySite(waitingPlayer);
            genpaiPlayer.CurrentRoundMonsterCount++;
        }

        public void SummonCancel()
        {
            if (waitingUnit != null)
            {
                waitingUnit.GetComponent<CardDisplay>().SelectedCancel();//取消召唤时大小恢复
            }
            summonWaiting = false;
            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
        }

        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="summonCard"></param>
        /// <param name="targetBucket">召唤目标格子</param>
        /// <param name="isP2">是否为P2（控制朝向）</param>
        public void Summon(UnitCard summonCard, GameObject targetBucket, bool isP2)
        {
            // 生成实际UnitEntity
            Transform unitSeats = targetBucket.transform.Find("Unit");
            GameObject unit = Object.Instantiate(PrefabsLoader.Instance.unitPrefab, unitSeats.transform);
            unit.SetActive(false);

            if (isP2 == true)
            {
                unit.transform.Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/HPCanvas/HPText").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/AtkCanvas/AtkText").Rotate(new Vector3(0, 180, 0));
                unit.transform.Find("UI/UnitUI/AtkCanvas/AttackEle").Rotate(new Vector3(0, 180, 0));
            }

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(waitingPlayer, targetBucket.GetComponent<BucketEntity>());


            BucketEntityManager.Instance.SetBucketCarryFlag(targetBucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());


            int serial = targetBucket.GetComponent<BucketEntity>().serial;
            Service.BattleField.Bucket newBucket = BattleFieldManager.Instance.GetBucketBySerial(serial);

            Service.Unit.Unit newUnit = new Service.Unit.Unit(summonCard, newBucket);
            AnimatorManager.Instance.InsertAnimatorTimeStep(AnimatorGenerator.GenerateSummonTimeStep(unit, newUnit));
            
        }


        /// <summary>
        /// 实行召唤
        /// </summary>
        /// <param name="unitCard">召唤参考单位卡（可修改为依ID读数据库）</param>
        /// <param name="targetBucket">召唤目标格子</param>
        /// <param name="isP2">是否为P2（控制朝向）</param>
        private void Summon(GameObject unitCard, GameObject targetBucket, bool isP2)
        {
            // 获取卡牌数据
            UnitCard summonCard = unitCard.GetComponent<CardDisplay>().Card as UnitCard;
            Summon(summonCard, targetBucket, isP2);
            unitCard.SetActive(false);
            
            //召唤成功，目标卡牌从手牌移除,整理剩余手牌
            GenpaiPlayer player = GameContext.GetPlayerBySite(waitingPlayer);
            player.HandCardManager.HandCardSort(unitCard);

        }
    }
}