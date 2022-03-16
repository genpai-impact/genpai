using Messager;
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
            List<int> cardIdList = CardLibrary.Instance.UserCardDeck[GameContext.MissionConfig.UserCardDeckId].CardIdList;
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

            GameContext.Player1.HandCharaManager.Summon();
            GameContext.Player2.HandCharaManager.Summon();
            GameContext.Player1.CharaCD = 0;
            GameContext.Player2.CharaCD = 0;

            // todo boss也从MissionConfig里读取
            InitBoss();
            NormalProcessManager.Instance.Next();
        }


        public void InitBoss()
        {
            // 召唤一个草率的Boss
            // 获取Boss卡牌数据
            UnitCard BossCard = CardLoader.Instance.GetCardById(401) as UnitCard;

            GameObject Bucket = BattleFieldManager.Instance.GetBucketBySerial(0);
            Debug.Log(Bucket);
            // 生成实际UnitEntity
            Transform UnitSeats = Bucket.transform.Find("Unit");
            
            GameObject unit = GameObject.Instantiate(PrefabsLoader.Instance.unitPrefab, UnitSeats.transform);

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(BossCard, BattleSite.Boss, Bucket.GetComponent<BucketEntity>());
            unit.GetComponent<UnitDisplay>().Init();


            BattleFieldManager.Instance.SetBucketCarryFlag(Bucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());

            GameContext.TheBoss = unit.GetComponent<UnitEntity>();
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}
