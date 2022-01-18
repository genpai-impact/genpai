﻿using Messager;
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
        /// <summary>
        /// 起始手牌数量
        /// </summary>
        private const int _startCardCount = 4;
        /// <summary>
        /// 起手英雄数量
        /// </summary>
        private const int _startHeroCount = 2;
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
            //测试部分写的烂，请折叠
            List<int> charaid = new List<int> { 100, 101, 102,101}; //测试
            List<int> monsterid = new List<int> { 200, 201, 202, 203, 204,200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204, 200, 201, 202, 203, 204 };//测试

            //根据一个内存中加载好的 用户/NPC 创建玩家 GenpaiPlayer(100, 4, 30, 0)有详细注释
            GenpaiPlayer genpaiPlayer = new GenpaiPlayer(100, 4, 30, 0);
            {//建议折叠
                string ids = "";
                foreach (int id in genpaiPlayer.ownCardIDList)
                {
                    ids += id + " ";
                }
                Debug.Log("玩家拥有的卡牌id："+ids);
            } 
            //玩家进行选牌的接口：传入所选的角色、怪物、毒药（未定）列表 if检查选择的数量是否正确
            if (!(genpaiPlayer.SelectCharaCard(charaid) && genpaiPlayer.SelectMonsterCard(monsterid))) {
                Debug.LogError("选择卡牌数目错误！");
            }
            {//建议折叠
                string sids = "";
                foreach (int id in genpaiPlayer.selectedCardIDList)
                {
                    sids += id + " ";
                }
                Debug.Log("玩家选择的卡牌id：" + sids);
            }

            //创建一个游戏场景（对战）上下文，设置第一个玩家
            GameContext gameContext = new GameContext();
            GameContext.Player1 = genpaiPlayer;
            //生成卡组(已随机处理)
            GameContext.Player1.CardDeck = new CardDeck(genpaiPlayer) ;
            {//测试,建议折叠
                CardDeck cardDeck = GameContext.Player1.CardDeck;
                Debug.Log("----gamestart------");
                string charaBrief = "角色牌库：\n", cardBrief = "手牌牌库：\n";
                
                var temp = cardDeck.CharaLibrary.First;
                do {
                    charaBrief += temp.Value.cardID + "  " + temp.Value.cardName + "\n";
                    temp = temp.Next;
                } while (temp!=null);
                Debug.Log(charaBrief);
                var temp1 = cardDeck.CardLibrary.First;
                do
                {
                    cardBrief += temp1.Value.cardID + "  " + temp1.Value.cardName + "\n";
                    temp1 = temp1.Next;
                } while (temp1 != null);
                Debug.Log(cardBrief);
            }


            /*for (int i = 0; i < _startHeroCount; i++)
            {  
                //GameContext.Player2.CardDeck.DrawHero();
            }
            for (int i = 0; i < _startCardCount; i++)
            {
                // GameContext.Player1.CardDeck.DrawCard();
                // GameContext.Player2.CardDeck.DrawCard();
            }*/
            //GameContext.processManager.Next();
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}
