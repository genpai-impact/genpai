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
        public int playerLevel;// 除1000
        public int playerId;

        public PlayerType playerType;   // 玩家类型：玩家，AI，互联网对手
        public PlayerSite playerSite;   // P1，P2，Boss

        public List<int> ownCardIDList;//挑选卡组前


        public int charanum;
        public int monsternum;
        public int spellnum;
        public List<int> selectedCardIDList;//挑选卡组后

        /// <summary>
        /// 构造一个可上场的player
        /// numofchara：要抽几个角色；monster：要抽几个怪；spell：几个法术
        /// 目前推荐：4,30,0
        /// </summary>
        public GenpaiPlayer(int _playerId, int numOfChara, int numOfMonster, int numOfSpell)
        {
            this.playerId = _playerId;
            //Player temp = PlayerLoader.Instance.GetPlayById(_playerId);
            //this.playerName = temp.playerName;
            //this.playerId = temp.playerId;
            //this.playerLevel = temp.playerLevel;
            //this.playerType = temp.playerType;
            //this.ownCardIDList = temp.ownCardIDList;
            this.charanum = numOfChara;
            this.monsternum = numOfMonster;
            this.spellnum = numOfSpell;
        }

        /// <summary>
        /// 游戏中选择上场的卡后调用此函数进行选择
        /// selectCardsId：卡号列表,可以重复！
        /// 可选角色，可选怪物,可选法书的数目为定值！
        /// 返回值对输入验证
        /// </summary>
        public bool SelectCharaCard(List<int> selectCardsId)
        {

            //Debug.Log(selectCardsId.ToString());
            if (selectCardsId.Count != charanum)
                return false;
            this.selectedCardIDList = new List<int>(selectCardsId);

            return true;
        }
        /// <summary>
        /// 游戏中选择上场的卡后调用此函数进行选择
        /// selectCardsId：卡号列表,可以重复！
        /// 可选角色，可选怪物,可选法书的数目为定值！
        /// 返回值对输入验证
        /// </summary>
        public bool SelectMonsterCard(List<int> selectCardsId, int n = 30)
        {
            if (selectCardsId.Count != monsternum)
            {
                return false;
            }
            this.selectedCardIDList.AddRange(selectCardsId);
            return true;
        }
        public bool SelectSpellCard(List<int> selectCardsId, int n)
        {
            return false;
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

        /// <summary>
        /// 当前是第几回合
        /// </summary>
        public int CurrentRound
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{{{nameof(GenpaiController)}={GenpaiController}, {nameof(CardDeck)}={CardDeck}, {nameof(CurrentRound)}={CurrentRound.ToString()}}}";
        }
    }
}
