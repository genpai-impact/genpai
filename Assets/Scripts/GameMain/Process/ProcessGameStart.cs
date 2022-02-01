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

            GameContext.Player2 = new GenpaiPlayer(201, BattleSite.P2);
            GameContext.Player2.Init();
            GameContext.Player2.CardDeck = new CardDeck();
            GameContext.Player2.CardDeck.Init(cardIdList, GameContext.Player2);

            GameContext.CurrentPlayer = GameContext.Player1;
            GameContext.LocalPlayer = GameContext.Player1;
            // 创建Boss
            // 为双方玩家牌库初始化配置（set抽卡数）
            GameContext.Player1.CardDeck.HandOutCard(2);
            GameContext.Player2.CardDeck.HandOutCard(2);

            NormalProcessManager.Instance.Next();
        }



        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}
