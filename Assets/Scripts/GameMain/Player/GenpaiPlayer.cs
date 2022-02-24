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

        public List<Chara> CharaList = new List<Chara>();

        public GameObject Chara;
        public BucketEntity CharaBucket;

        public int CharaCD;


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
            InitCharaSeat();
        }

        public void InitCharaSeat()
        {

            if (playerSite == BattleSite.P1)
            {
                CharaBucket = BattleFieldManager.Instance.GetBucketBySerial(5).GetComponent<BucketEntity>();
            }
            else
            {
                CharaBucket = BattleFieldManager.Instance.GetBucketBySerial(12).GetComponent<BucketEntity>();
            }

            Transform UnitSeats = CharaBucket.transform.Find("Unit");

            Chara = GameObject.Instantiate(PrefabsLoader.Instance.unitPrefab, UnitSeats.transform);

            if (playerSite == BattleSite.P2)
            {
                Chara.transform.Rotate(new Vector3(0, 180, 0));
            }

            Chara.AddComponent<UnitEntity>();
            Chara.AddComponent<UnitPlayerController>();
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
        public HandCharaManager HandCharaManager = new HandCharaManager();

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

            for (int i = 0; i < charaN; i++)
            {
                Card drawedCard = CardDeck.DrawChara();

                HandCharaManager.Instantiate(drawedCard, playerSite);

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
                Card drawedCard = CardDeck.DrawCard();

                if (drawedCard != null)
                {
                    GameObject obj = HandCardManager.Instantiate(drawedCard, playerSite);
                    obj.GetComponent<CardPlayerController>().playerSite = this.playerSite;
                    HandCardManager.MoveToPool(obj);
                }


            }
            return ret;
        }
    }
}
