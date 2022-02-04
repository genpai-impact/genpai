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

        // 这些配置信息可以考虑设置于上下文中，读取textasset创建（待实现）
        /// <summary>
        /// 起始手牌数量
        /// </summary>
        public const int _startCardCount = 4;
        /// <summary>
        /// 起手英雄数量
        /// </summary>
        public const int _startHeroCount = 2;
        /// <summary>
        /// 每回合抽卡数量
        /// </summary>
        public const int _roundCardCount = 1;

        private ProcessGameStart()
        {
            //GenpaiPlayer player1 = new GenpaiPlayer()
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
            // 创建双方玩家
            List<int> cardIdList = new List<int> { 100, 101, 102, 101, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204 };//测试


            GameContext.Player1 = new GenpaiPlayer(200, BattleSite.P1);

            GameContext.Player1.Init();

            // 初始化牌组部分待移动至PlayerInit内
            GameContext.Player1.CardDeck = new CardDeck();

            GameContext.Player1.CardDeck.Init(cardIdList, GameContext.Player1);
            LibraryTest(GameContext.Player1.CardDeck);

            GameContext.Player2 = new GenpaiPlayer(201, BattleSite.P2);
            GameContext.Player2.Init();
            GameContext.Player2.CardDeck = new CardDeck();
            GameContext.Player2.CardDeck.Init(cardIdList, GameContext.Player2);
            LibraryTest(GameContext.Player2.CardDeck);

            GameContext.CurrentPlayer = GameContext.Player1;
            GameContext.LocalPlayer = GameContext.Player1;
            // 创建Boss
            // 为双方玩家牌库初始化配置（set抽卡数）

            // 发布游戏开始消息（牌库实现抽卡）
            GameContext.Player1.HandOutChara(2);
            GameContext.Player1.HandOutCard(4);

            GameContext.Player2.HandOutChara(2);
            GameContext.Player2.HandOutCard(4);

            NormalProcessManager.Instance.Next();

            // 召唤一个草率的Boss
            // 获取Boss卡牌数据
            UnitCard BossCard = CardLoader.Instance.GetCardById(401) as UnitCard;

            GameObject Bucket = BattleFieldManager.Instance.GetBucketBySerial(0);

            // 生成实际UnitEntity
            Transform UnitSeats = Bucket.transform.Find("Unit");
            GameObject unit = GameObject.Instantiate(processtest.Instance.unitPrefab, UnitSeats.transform);

            unit.AddComponent<UnitEntity>();
            unit.AddComponent<UnitPlayerController>();

            unit.GetComponent<UnitEntity>().Init(BossCard, BattleSite.Boss, Bucket.GetComponent<BucketEntity>());
            unit.GetComponent<UnitDisplay>().Init();


            BattleFieldManager.Instance.SetBucketCarryFlag(Bucket.GetComponent<BucketUIController>().bucket.serial, unit.GetComponent<UnitEntity>());

            GameContext.TheBoss = unit.GetComponent<UnitEntity>();

        }




        public void LibraryTest(CardDeck cardDeck)
        {
            Debug.Log("----牌库打印：------");
            string charaBrief = "角色牌库：\n", cardBrief = "手牌牌库：\n";

            var temp = cardDeck.CharaLibrary.First;
            do
            {
                charaBrief += temp.Value.cardID + "  " + temp.Value.cardName + "\n";
                temp = temp.Next;
            } while (temp != null);
            Debug.Log(charaBrief);
            var temp1 = cardDeck.CardLibrary.First;
            do
            {
                cardBrief += temp1.Value.cardID + "  " + temp1.Value.cardName + "\n";
                temp1 = temp1.Next;
            } while (temp1 != null);
            Debug.Log(cardBrief);
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}
