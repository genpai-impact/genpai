﻿using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Genpai
{
    /// <summary>
    /// 游戏开始时需要做的事
    /// </summary>
    class ProcessGameStart : IProcess
    {
        private static ProcessGameStart gameStartProcess = new ProcessGameStart();

        private ProcessGameStart()
        {
        }
        public static string NAME = "GameStart";
        public static ProcessGameStart GetInstance()
        {
            return gameStartProcess;
        }

        public string GetName()
        {
            return NAME;
        }

        public void Run()
        {
            GameContext.Player1 = new GenpaiPlayer(200, BattleSite.P1);
            GameContext.Player1.Init();
            GameContext.Player2 = new GenpaiPlayer(201, BattleSite.P2);
            GameContext.Player2.Init();

            GameContext.CurrentPlayer = GameContext.Player1;
            GameContext.LocalPlayer = GameContext.Player1;
            // 创建Boss
            // 为双方玩家牌库初始化配置（set抽卡数）

            // 发布游戏开始消息（牌库实现抽卡）
            GameContext.Player1.HandOutChara(GameContext.MissionConfig.StartHeroCount);
            GameContext.Player1.HandOutCard(GameContext.MissionConfig.StartCardCount);

            GameContext.Player2.HandOutChara(GameContext.MissionConfig.StartHeroCount);
            GameContext.Player2.HandOutCard(GameContext.MissionConfig.StartCardCount);

            GameContext.Player1.CharaManager.Summon(true);
            GameContext.Player2.CharaManager.Summon(true);
            GameContext.Player1.CharaManager.CurrentCharaBanner.gameObject.SetActive(true);
            GameContext.Player2.CharaManager.CurrentCharaBanner.gameObject.SetActive(true);
            GameContext.Player1.CharaCD = 0;
            GameContext.Player2.CharaCD = 0;

            InitBoss();
            NormalProcessManager.Instance.Next();
        }


        public void InitBoss()
        {
            // 获取Boss卡牌数据
            UnitCard BossCard = CardLoader.Instance.GetCardById(GameContext.MissionConfig.BossID) as UnitCard;
            GameObject Bucket = BucketEntityManager.Instance.GetBucketBySerial(0);
            Transform UnitSeats = Bucket.transform.Find("Unit");
            GameObject unit = GameObject.Instantiate(PrefabsLoader.Instance.unitPrefab, UnitSeats.transform);
            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();
            unit.GetComponent<UnitEntity>().Init(BattleSite.Boss, Bucket.GetComponent<BucketEntity>());

            BucketEntityManager.Instance.SetBucketCarryFlag(Bucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());




            Bucket newBucket = BattleFieldManager.Instance.GetBucketBySerial(0);
            Unit newUnit = new Boss(BossCard, newBucket);
            unit.GetComponent<UnitDisplay>().Init(newUnit.GetView());
            GameContext.TheBoss = newUnit as Boss;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}