using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using cfg;
namespace Genpai
{
    /// <summary>
    /// 卡牌显示，通过UnityEngine.UI修改卡牌模板
    /// </summary>
    public class GroupCardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
    {
        /// <summary>
        /// 待显示卡牌
        /// </summary>
        public Card card;
      //  public UnitInfoDisplay UID;
        /// <summary>
        /// 基础卡牌信息
        /// </summary>
        public Text cardName;
        public Image cardImage;
        [SerializeField]
        public int CardNums;
        public enum CardStatus//卡组选牌状态
        {
            Down,
            Up
        };
        public CardStatus cardStatus;
        /// <summary>
        /// 单位卡信息容器显示
        /// </summary>
        public GameObject UnitCanvas;
        public Text atkText;
        public Text hpText;
        public Text numText;
        public Image atkElement;
        [SerializeField]
        private bool canShow = false;
        private bool isGary;
        bool isChar;
        private CardGroupManager manager;
        /// <summary>
        /// 悬浮显示相关
        /// </summary>
        private Vector3 _ObjectScale;

        void Start()
        {
            manager = transform.parent.parent.parent.parent.parent.GetComponent<CardGroupManager>();
            cardImage.transform.localScale = new Vector3(0.7f, 0.7f, 1);
           // UID = GameObject.Find("UnitInfo").GetComponent<UnitInfoDisplay>();
            _ObjectScale = gameObject.transform.localScale;
            if (card != null)
            {
                DisplayCard();
            }
            isChar = card.cardType == cfg.card.CardType.Chara;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            canShow = true;
            //UID.Init()
          //  Debug.Log("enter");
           
            Zoom();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            canShow = false;
            gameObject.transform.localScale = _ObjectScale;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                        switch (this.cardStatus)
                        {
                            case CardStatus.Down:
                                Down2Up(this.gameObject);
                                break;
                            case CardStatus.Up:
                                Up2Down(this.gameObject);
                                break;
                        }
            }
                
            Debug.Log(CardNums);
            
           
          //  Debug.Log("aaa");
            //throw new System.NotImplementedException();
        }
        
        public void Zoom()
        {
            gameObject.transform.localScale = new Vector3(1.2f * _ObjectScale.x, 1.2f * _ObjectScale.y, 1);
            //            Debug.Log("放大");
        }

        public void Revert()
        {
            gameObject.transform.localScale = _ObjectScale;
        }
        private void Down2Up(GameObject gameObject)
        {
            if (CardNums == 0) return;
            if (!isChar && manager.AllCardNums == manager.MaxCardNums) return;
            if (isChar && manager.CharNums == manager.MaxCharNums) return;
                // Debug.Log(.name);
                if(!manager.SelectCard.ContainsKey(card.cardID))
                {
                    manager.SelectCard.Add(card.cardID, 1);
                    GameObject RightObject = null;// Instantiate(manager.prefab, manager.RightCards.transform);
                    switch (UnitInfoDisplay.Instance.DIRECTORY[card.cardName])
                    {
                        case "角色":
                            RightObject = Instantiate(manager.prefab, manager.RightCards.transform.transform.GetChild(0));
                            break;
                        case "丘丘人":
                            RightObject = Instantiate(manager.prefab, manager.RightCards.transform.transform.GetChild(1));
                            break;
                        case "史莱姆":
                            RightObject = Instantiate(manager.prefab, manager.RightCards.transform.transform.GetChild(2));
                            break;

                    }
                    
                    RightObject.name = card.cardID.ToString();
                    GroupCardDisplay GCD = RightObject.GetComponent<GroupCardDisplay>(); ;
                    GCD.cardStatus = CardStatus.Up;
                   // GCD.UID = UID;
                    GCD.cardName.text = card.cardName;
                    GCD.card = card;
                   
                    GCD.CardNums = 1;
                    GCD.numText.text = "1";
                    //RightObject.GetComponent<GroupCardDisplay>().CardNums = 1;
                    Debug.Log(GCD.CardNums);
                }
                else
                {
                    manager.SelectCard[card.cardID]++;
                    GroupCardDisplay GCD = 
                        manager.RightCards.transform.Find(UnitInfoDisplay.Instance.DIRECTORY[card.cardName]).Find(card.cardID.ToString()).GetComponent<GroupCardDisplay>();
                    GCD.CardNums++;
                    GCD.numText.text = GCD.CardNums.ToString();

                }
               
                CardNums--;
                if (!isChar) manager.AllCardNums++;
                else manager.CharNums++;
                numText.text = CardNums.ToString();

                manager.CurCardStage.text = manager.AllCardNums + "/" + manager.MaxCardNums;
                manager.CharCardStage.text = manager.CharNums + "/" + manager.MaxCharNums;
            
        }
        private void Up2Down(GameObject gameObject)
        {


                manager.SelectCard[card.cardID]--;
            if (manager.SelectCard[card.cardID] == 0)
            {
                manager.SelectCard.Remove(card.cardID);
            }
                CardNums--;
            if (!isChar) manager.AllCardNums--;
            else manager.CharNums--;

                numText.text = CardNums.ToString();
                
                manager.CurCardStage.text = manager.AllCardNums + "/" + manager.MaxCardNums;
                manager.CharCardStage.text= manager.CharNums + "/" + manager.MaxCharNums; ;
            GameObject LeftObject = manager.LeftCards.transform.Find(UnitInfoDisplay.Instance.DIRECTORY[card.cardName]).Find(card.cardID.ToString()).gameObject;
            GroupCardDisplay GCD = LeftObject.GetComponent<GroupCardDisplay>();
            GCD.CardNums++;
            GCD.numText.text = GCD.CardNums.ToString();
            if (CardNums == 0) Destroy(this.gameObject);

        }
        
        public void Update()
        {
            if (Input.GetMouseButtonDown(1) && canShow)
            {
                UnitInfoDisplay.Instance.GCDInit(this);
                UnitInfoDisplay.Instance.ReDraw_Card(this);
            }
           // CardColorChange();
        }

     

        public void DisplayUnitCard(UnitCard unitcard)
        {
            atkText.text = unitcard.atk.ToString();
            hpText.text = unitcard.hp.ToString();
            UnitCanvas.gameObject.SetActive(true);
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "UnitModel/ModelImage/" + card.cardName;
                float imageSizeScale = 1f;
                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
                if(cardStatus == CardStatus.Down)
                {
                  CardNums = UserLoader.Instance.cardInfo[unitcard.cardID];
                  numText.text = CardNums.ToString();
                }
              
            }
            catch
            {
                Debug.Log(card.cardName + " 无模型");
            }
        }

        public void DisplaySpellCard()
        {
            try
            {
                // 使用Resources.Load方法，读取Resources文件夹下模型
                // 目前使用卡名直接读取，待整理资源格式
                // TODO
                string imgPath = "ArtAssets/Card/魔法牌/" + card.cardName;

                float imageSizeScale = 1f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(card.cardName + " 无模型");
            }
        }

        /// <summary>
        /// 显示卡牌：将卡牌数据与UI绑定
        /// </summary>
        public void DisplayCard()
        {
            // 默认关闭数值表
            UnitCanvas.gameObject.SetActive(false);

            // 加载卡名&描述
            cardName.text = card.cardName;

            if (card is UnitCard)
            {
                var unitcard = card as UnitCard;
                DisplayUnitCard(unitcard);
            }
            else if (card is SpellCard||(card is NewSpellCard))
            {
               
                DisplaySpellCard();
            }
        }

       
    }
}