using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Genpai
{
    public class CardGroupManager : MonoSingleton<CardGroupManager>
    {
        public GameObject CharaCards;//人物栏
        public GameObject MonsterCards;//丘丘人栏
        public GameObject ShiLaiMuCards;//史莱姆栏
        public GameObject All;
        public GameObject LeftCards;
        public GameObject RightCards;//右侧 已选手牌

        public GameObject prefab;
        public GameObject prefabRight;

        public Dictionary<int,int> StageCard;//拥有的卡牌+数量
        public TextAsset cardData; // 卡牌数据Json
        public Dictionary<int,int> SelectCard = new Dictionary<int,int>();

        public int AllCardNums;
        public Text CurCardStage;
        public Text CharCardStage;
        public int MaxCardNums=24;
        public int CharNums;
        public int MaxCharNums=2;
        public bool isConfig = false;
        public bool btnClick;//检测Tag按钮
        void Start()
        {
           // LubanLoader.Init();
            // UID = transform.GetChild(transform.childCount-1).GetChild(0).GetComponent<UnitInfoDisplay>();
            StageCard = new Dictionary<int, int>();
            foreach (var id in UserLoader.Instance.ownCardIDList)
            {
                //Debug.Log(id);
               // Debug.Log((Card)CardLoader.Instance.CardList[id]);
                StageCard.Add(id, UserLoader.Instance.cardInfo[id]);
              //  Debug.Log(StageCard[StageCard.Count - 1]);
            }
           // Debug.Log(StageCard.Count);
            groupInit();
            CurCardStage.text = AllCardNums + "/" + MaxCardNums;
            CharCardStage.text = CharNums + "/" + MaxCharNums;
            GroupCardDisplay firstCard = LeftCards.transform.GetChild(3).GetChild(0).GetComponent<GroupCardDisplay>();
            //UnitInfoDisplay.Instance.GCDInit(firstCard);
            //UnitInfoDisplay.Instance.ReDraw_Card(firstCard);
        }
        
        // Update is called once per frame
        void Update()
        {
           
            
        }
        private void LateUpdate()
        {
            if (btnClick)
            {
                // GroupCardDisplay[] gcdS = GameObject.FindObjectsOfType<GroupCardDisplay>();
                Object[] objS = Resources.FindObjectsOfTypeAll(typeof(GroupCardDisplay));
                //  Debug.Log(objS.Length);
                foreach (var obj in objS)
                {
                    if (obj.name != "GroupCard" && obj.name != "GroupCardRight")
                    {
                        GroupCardDisplay gcd = (GroupCardDisplay)obj;
                        if (gcd.gameObject.CompareTag("GroupCard"))
                        {
                            gcd.CardNums = StageCard[gcd.card.CardID];
                            Debug.Log(gcd.gameObject.transform.parent.parent.name + " " + gcd.card.CardID + " " + gcd.CardNums);
                            gcd.numText.text = gcd.CardNums.ToString();
                            Debug.Log(gcd.numText.text);
                        }

                    }
                }
                btnClick = false;
            }
        }
        void OnDestroy()
        {
            //SingletonKiller s = new SingletonKiller();
            //s.KillMonoSingletonAll(true);
            //s.KillSingletonAll(true);
            
        }

        void groupInit()
        {
            foreach(var id in StageCard.Keys)
            {
                //Debug.Log("aa" + StageCard.Count);
                //Debug.Log("aa" + StageCard[i].cardName);
                //Debug.Log(i);
                GameObject _card=null;
                GameObject _all = null;
                Card card = (Card)CardLoader.Instance.CardList[id];
                switch (UnitInfoDisplay.Instance.DIRECTORY[card.CardName] )
                {
                    case "角色": 
                       _card =  Instantiate(prefab, CharaCards.transform);
                        break;
                    case "丘丘人":
                        _card = Instantiate(prefab, MonsterCards.transform);
                        break;
                    case "史莱姆":
                        _card = Instantiate(prefab, ShiLaiMuCards.transform);
                        break;

                }
                _all= Instantiate(prefab, All.transform);
                
                getCardInfo(_card, card);
                getCardInfo(_all, card);
            }
            
        }
        void getCardInfo(GameObject obj,Card card)
        {
            obj.name = card.CardID.ToString();
            GroupCardDisplay GCD = obj.GetComponent<GroupCardDisplay>();
            GCD.cardStatus = GroupCardDisplay.CardStatus.Down;
            //GCD.UID = UID;
            GCD.cardName.text = card.CardName;
            GCD.card = card;
            //GRP.cardImage=StageCard[i].
            // GRP.atkText.text=StageCard[i].
            if (card is UnitCard)
            {
                UnitCard _unitcard = card as UnitCard;
            }
        }
        /*以下为按钮事件*/
        public void btnAction()
        {
            btnClick = true;

        }
        public void btnConfig()
        {
            isConfig = true;
        }
       
       
       
    }
}
