﻿using System.Collections.Generic;
using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.EntityController;
using BattleSystem.Controller.EntityManager;
using BattleSystem.Controller.Unit;
using BattleSystem.Service.Card;
using BattleSystem.Service.Common;
using BattleSystem.Service.Unit;
using cfg.level;
using DataScripts.DataLoader;
using UnityEngine;



    /// <summary>
    /// 游戏中的玩家信息
    /// </summary>
    public class GenpaiPlayer : User
    {
        public string playerName;
        public int playerId;

        public PlayerType playerType;   // 玩家类型：玩家，AI，互联网对手
        public BattleSite playerSite;   // P1，P2，Boss

        public List<Chara> CharaList = new List<Chara>();

        public Chara Chara;
        public GameObject CharaObj;
        public BucketEntity CharaBucket;

        public int CharaCD;
        public int CurrentRoundMonsterCount;

        public HandCardManager HandCardManager = new HandCardManager();
        public CharaManager CharaManager = new CharaManager();
        public BucketManager bucketManager = new BucketManager();
        public SummonedManager summonedManager = new SummonedManager();


    /// <summary>
    /// 势力枚举
    /// </summary>
    public enum BattleSite
    {
        P1,
        P2,
        Boss
    }


    /// <summary>
    /// 玩家类型
    /// </summary>
    public enum PlayerType
    {
        Human,
        AI,
        InternetHuman // 当前还不支持联网，所以不存在网络对战玩家
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


        /// <summary>
        /// 构造一个可上场的player
        /// numofchara：要抽几个角色；monster：要抽几个怪；spell：几个法术
        /// 目前推荐：4,30,0
        /// </summary>

        public GenpaiPlayer(int _playerId, BattleSite _playerSite,PlayerType playerType)
        {
            User temp = PlayerLoader.Instance.GetPlayById(_playerId);
            this.playerName = temp.playerName;
            this.playerId = temp.playerId;
            this.playerType = playerType;
            this.playerSite = _playerSite;
        }



        
        public void Init()
        {
            CharaManager.Init(playerSite);
            InitCardDeck();
            GenpaiController = new GenpaiController();
            InitCharaSeat();
        }
        
        /// <summary>
        /// 卡组初始化
        /// </summary>
        private void InitCardDeck()
        {
            CardDeck = new CardDeck();

            var cardLibraryId = playerSite == BattleSite.P1
                ? GameContext.MissionConfig.UserCardLibraryId
                : GameContext.MissionConfig.EnemyCardLibraryId;

            var cardLibrary = CardLibraryLoader.Instance.GetCardLibrary(playerSite, cardLibraryId);
            
            CardDeck.Init(cardLibrary, this);
        }

        private void InitCharaSeat()
        {
            // 获取角色格子
            if (playerSite == BattleSite.P1)
            {
                CharaBucket = BucketEntityManager.Instance.GetBucketBySerial(5).GetComponent<BucketEntity>();
            }
            else
            {
                CharaBucket = BucketEntityManager.Instance.GetBucketBySerial(12).GetComponent<BucketEntity>();
            }

            Transform UnitSeats = CharaBucket.transform.Find("Unit");


            // 获取召唤角色
            CharaObj = GameObject.Instantiate(PrefabsLoader.Instance.unitPrefab, UnitSeats.transform);

            if (playerSite == BattleSite.P2)
            {
                CharaObj.transform.Rotate(new Vector3(0, 180, 0));

                CharaObj.transform.Find("UI/UnitUI/HPCanvas/Image").Rotate(new Vector3(0, 180, 0));
                CharaObj.transform.Find("UI/UnitUI/HPCanvas/HPText").Rotate(new Vector3(0, 180, 0));

                CharaObj.transform.Find("UI/UnitUI/AtkCanvas/AttackEle").Rotate(new Vector3(0, 180, 0));
                CharaObj.transform.Find("UI/UnitUI/AtkCanvas/Image").Rotate(new Vector3(0, 180, 0));
                CharaObj.transform.Find("UI/UnitUI/AtkCanvas/AtkText").Rotate(new Vector3(0, 180, 0));

                CharaObj.transform.Find("UI/UnitUI/EngCanvas/Image").Rotate(new Vector3(0, 180, 0));
                CharaObj.transform.Find("UI/UnitUI/EngCanvas/EngText").Rotate(new Vector3(0, 180, 0));
            }

            CharaObj.AddComponent<UnitEntity>();
            CharaObj.AddComponent<UnitPlayerController>();
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

            for (int i = 0; i < charaN; i++)
            {
                DataScripts.Card.Card drawedCard = CardDeck.DrawChara();

                CharaManager.AddChara(drawedCard);

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
            if (CardDeck.HandCardList.Count + cardN > GameContext.MissionConfig.S_HandCardLimit)
            {
                ret = 0;
            }
            for (int i = 0; i < cardN; i++)
            {
                DataScripts.Card.Card drawedCard = CardDeck.DrawCard();

                if (drawedCard != null)
                {
                    GameObject obj = HandCardManager.Instantiate(drawedCard, playerSite);
                    obj.GetComponent<CardPlayerController>().playerSite = this.playerSite;
                    obj.GetComponent<CardPlayerController>().Card = drawedCard;

                    HandCardManager.MoveToPool(obj);
                }


            }
            return ret;
        }

        public void SubCharaCD()
        {
            CharaCD--;
            if (CharaCD < 0)
            {
                CharaCD = 0;
            }
        }
    }
