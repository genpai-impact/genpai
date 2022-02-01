using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 游戏中的玩家信息
    /// </summary>
    public class GenpaiPlayer
    {
        public string playerName;
        public int playerId;

        public PlayerType playerType;   // 玩家类型：玩家，AI，互联网对手
        public BattleSite playerSite;   // P1，P2，Boss

        /// <summary>
        /// 构造一个可上场的player
        /// numofchara：要抽几个角色；monster：要抽几个怪；spell：几个法术
        /// 目前推荐：4,30,0
        /// </summary>
        public GenpaiPlayer(int _playerId, BattleSite _playerSite)
        {
            Player temp = PlayerLoader.Instance.GetPlayById(_playerId);
            this.playerName = temp.playerName;
            this.playerId = temp.playerId;
            this.playerType = temp.playerType;
            this.playerSite = _playerSite;
        }

        public void Init() 
        { 
            GenpaiController = new GenpaiController();
        }

        /// <summary>
        /// 控制者
        /// </summary>
        public GenpaiController GenpaiController
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家的卡组
        /// </summary>
        public CardDeck CardDeck
        {
            get;
            set;
        }

        public HandCardManager HandCardManager = new HandCardManager();
        

        /// <summary>
        /// 当前是第几回合
        /// </summary>
        public int CurrentRound
        {
            get;
            set;
        }

        /// <summary>
        /// 分发角色
        /// </summary>
        public int HandOutChara(int charaN)
        {
            int ret = 1;
            if (charaN > CardDeck.CharaLibrary.Count)
            {
                charaN = CardDeck.CharaLibrary.Count;
                ret = -1;
            }
            /*if (CardDeck.HandCardList.Count + charaN > CardDeck.S_HandCardLimit)
            {
                ret = 0;
            }*/
            for (int i = 0; i < charaN; i++)
            {
                Card drawedCard = CardDeck.DrawChara();

                drawedCard.blongTo = this.playerSite;
                // Unit temp = new Chara(DrawedChara as UnitCard);
                // TODO：将角色塞入玩家列表
                //GameObject newCard = GameObject.Instantiate(processtest.Instance.cardPrefab, processtest.Instance.cardPool.transform);

                GameObject obj = HandCardManager.Instantiate(drawedCard);
                //obj.GetComponent<CardPlayerController>().playerSite = this.playerSite;
                //HandCardManager.MoveToPool(obj);
            }
            return ret;
        }


        /// <summary>
        /// 尽最大可能发/摸牌（！不是抽牌） charaN:发放角色数 cardN：发放卡牌数
        /// 返回值：1 牌库够；-1 牌库不够；0：手牌溢出，依然发牌，需执行CheckAndCullCard()剔除操作
        /// </summary>
        public int HandOutCard(int cardN)
        {
            int ret = 1;
            if (cardN > CardDeck.CardLibrary.Count)
            {
                cardN = CardDeck.CardLibrary.Count;
                ret = -1;
            }
            if (CardDeck.HandCardList.Count + cardN > CardDeck.S_HandCardLimit)
            {
                ret = 0;
            }
            for (int i = 0; i < cardN; i++)
            {
                Card drawedCard=CardDeck.DrawCard();
                drawedCard.blongTo = this.playerSite;
                GameObject obj= HandCardManager.Instantiate(drawedCard);
                obj.GetComponent<CardPlayerController>().playerSite = this.playerSite;
                HandCardManager.MoveToPool(obj);
            }
            return ret;
        }
    }
}
