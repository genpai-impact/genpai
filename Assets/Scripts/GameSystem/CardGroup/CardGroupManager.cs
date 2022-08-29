using System.Collections.Generic;
using BattleSystem.Controller.Unit;
using DataScripts.Card;
using DataScripts.DataLoader;
using UnityEngine;
using UnityEngine.UI;
using Utils;
namespace GameSystem.CardGroup
{
    public class CardGroupManager : MonoSingleton<CardGroupManager>
    {
        public GameObject CharaCards;//人物栏
        public GameObject MonsterCards;//丘丘人栏
        public GameObject MagicCard;//魔法卡栏
        public GameObject All;
        public GameObject LeftCards;
        public GameObject RightCards;//右侧 已选手牌

        public GameObject prefab;
        public GameObject prefabRight;

        public Dictionary<int,int> StageCard;//拥有的卡牌+数量
        public TextAsset cardData; // 卡牌数据Json
        public static Dictionary<int,int> SelectCard = new Dictionary<int,int>();
        private UnitInfoDisplay UID;

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
            CardLoader.Instance.Init();
            StageCard = new Dictionary<int, int>();
            foreach (var id in UserLoader.Instance.ownCardIDList)
            {
                StageCard.Add(id, UserLoader.Instance.cardInfo[id]);
            }
            groupInit();
          //  GroupCardDisplay firstCard = LeftCards.transform.GetChild(3).GetChild(0).GetComponent<GroupCardDisplay>();
            RightCardsInit();
            Debug.Log("c" + LeftCards.transform.GetChild(4).GetChild(0).GetComponent<GroupCardDisplay>().CardNums);
            CurCardStage.text = AllCardNums + "/" + MaxCardNums;
            CharCardStage.text = CharNums + "/" + MaxCharNums;
        }
        void RightCardsInit()
        {
            if (SelectCard.Count == 0) return;
            else
            {
                foreach(var select in SelectCard.Keys)
                {
                    GameObject RightObject = null;
                    RightObject = Instantiate(prefabRight,RightCards.transform.transform.GetChild(3));
                    RightObject.name = select.ToString();
                    GroupCardDisplay left = LeftCards.transform.GetChild(4).Find(select.ToString()).GetComponent<GroupCardDisplay>();
                    Card card = left.card;
                    GroupCardDisplay GCD = RightObject.GetComponent<GroupCardDisplay>(); ;
                    GCD.cardStatus = GroupCardDisplay.CardStatus.Up;
                    GCD.cardName.text = card.CardName;
                    GCD.card = card;
                    GCD.CardNums = SelectCard[select];
                    GCD.numText.text = GCD.CardNums.ToString();
                    Debug.Log("a" + left.CardNums+" " + GCD.CardNums);
                    left.CardNums -= GCD.CardNums;
                    Debug.Log("b" + left.CardNums);
                    left.numText.text = left.CardNums.ToString();
                    if (card.CardType == cfg.card.CardType.Chara) CharNums += SelectCard[select];   
                    else AllCardNums += SelectCard[select];

                }
               
            }
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
                           // Debug.Log(gcd.gameObject.transform.parent.parent.name + " " + gcd.card.CardID + " " + gcd.CardNums);
                            gcd.numText.text = gcd.CardNums.ToString();
                          //  Debug.Log(gcd.numText.text);
                        }

                    }
                }
                btnClick = false;
            }
        }

        void groupInit()
        {
            Debug.Log("aa" + StageCard.Count);
            foreach (var id in StageCard.Keys)
            {
                //Debug.Log("aa" + StageCard.Count);
                //Debug.Log("aa" + StageCard[i].cardName);
                //Debug.Log(i);
                GameObject _card=null;
                GameObject _all = null;
                //Debug.Log("sss"+id);
                Card card = (Card)CardLoader.Instance.CardList[id];
              Debug.Log("选到的卡"+card.CardName);
                switch (UnitInfoDisplay.Instance.DIRECTORY[card.CardName] ) 
                {
                    case "角色": 
                       _card =  Instantiate(prefab, CharaCards.transform);
                        break;
                    case "丘丘人":
                        _card = Instantiate(prefab, MonsterCards.transform);
                        break;
                    case "史莱姆":
                        _card = Instantiate(prefab, MonsterCards.transform);
                        break;
                    case "丘丘萨满":
                        _card = Instantiate(prefab, MonsterCards.transform);
                        break;

                }
                _all= Instantiate(prefab, All.transform);
                Debug.Log("___");
                getCardInfo(_card, card);
                getCardInfo(_all, card);
            }
            
        }
        void getCardInfo(GameObject obj,Card card)
        {
            Debug.Log(obj.name+ " "+card.CardID);
            obj.name = card.CardID.ToString();
            GroupCardDisplay GCD = obj.GetComponent<GroupCardDisplay>();
            GCD.cardStatus = GroupCardDisplay.CardStatus.Down;
            //GCD.UID = UID;
            GCD.cardName.text = card.CardName;
            GCD.card = card;
           // GCD.CardNums = StageCard[card.CardID];
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
            if(SelectCard.Count>0)
            {

            }
        }
        public void btnCancel()
        {
            isConfig = false;
        }
       
       
       
    }
}
