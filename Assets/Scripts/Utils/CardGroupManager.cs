using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    public class CardGroupManager : MonoBehaviour
    {
        public GameObject CharaCards;//人物栏
        public GameObject MonsterCards;//丘丘人栏
        public GameObject ShiLaiMuCards;//史莱姆栏
        public GameObject LeftCards;
        public GameObject RightCards;//右侧 已选手牌

        public GameObject prefab;

        private List<Card> StageCard;//拥有的卡牌

        public UnitInfoDisplay UID;//二级菜单

        public TextAsset cardData; // 卡牌数据Json

        

        
        public Dictionary<int,int> SelectCard = new Dictionary<int,int>();

        public int AllCardNums;
        public Text CurCardStage;
        public int MaxCardNums=24;
        void Start()
        {
            LubanLoader.Init();
            // UID = transform.GetChild(transform.childCount-1).GetChild(0).GetComponent<UnitInfoDisplay>();
            StageCard = new List<Card>();
            foreach (var id in UserLoader.Instance.ownCardIDList)
            {
                //Debug.Log(id);
               // Debug.Log((Card)CardLoader.Instance.CardList[id]);
                StageCard.Add((Card)CardLoader.Instance.CardList[id]);
              //  Debug.Log(StageCard[StageCard.Count - 1]);
            }
           // Debug.Log(StageCard.Count);
            groupInit();
            CurCardStage.text = AllCardNums + "/" + MaxCardNums;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void groupInit()
        {
            for(int i=0;i<StageCard.Count;i++)
            {
                //Debug.Log("aa" + StageCard.Count);
                //Debug.Log("aa" + StageCard[i].cardName);
                //Debug.Log(i);
                GameObject _card=null;
                switch (UID.DIRECTORY[StageCard[i].cardName] )
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
                _card.name = StageCard[i].cardID.ToString();
                GroupCardDisplay GCD = _card.GetComponent<GroupCardDisplay>();
                GCD.cardStatus = GroupCardDisplay.CardStatus.Down;
                GCD.UID = UID;
                GCD.cardName.text = StageCard[i].cardName;
                GCD.card = StageCard[i];
                //GRP.cardImage=StageCard[i].
               // GRP.atkText.text=StageCard[i].
               if(StageCard[i] is UnitCard)
                {
                    UnitCard _unitcard = StageCard[i] as UnitCard;
                    //CardDisplay
                }
            }
            
        }
       
       
    }
}
