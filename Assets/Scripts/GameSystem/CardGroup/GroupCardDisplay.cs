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
    /// ������ʾ��ͨ��UnityEngine.UI�޸Ŀ���ģ��
    /// </summary>
    public class GroupCardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
    {
        /// <summary>
        /// ����ʾ����
        /// </summary>
        public Card card;
      //  public UnitInfoDisplay UID;
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public Text cardName;
        public Image cardImage;
        [SerializeField]
        public int CardNums;
        public enum CardStatus//����ѡ��״̬
        {
            Down,
            Up
        };
        public CardStatus cardStatus;
        /// <summary>
        /// ��λ����Ϣ������ʾ
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
        /// ������ʾ���
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
            //            Debug.Log("�Ŵ�");
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
                        case "��ɫ":
                            RightObject = Instantiate(manager.prefab, manager.RightCards.transform.transform.GetChild(0));
                            break;
                        case "������":
                            RightObject = Instantiate(manager.prefab, manager.RightCards.transform.transform.GetChild(1));
                            break;
                        case "ʷ��ķ":
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
                // ʹ��Resources.Load��������ȡResources�ļ�����ģ��
                // Ŀǰʹ�ÿ���ֱ�Ӷ�ȡ����������Դ��ʽ
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
                Debug.Log(card.cardName + " ��ģ��");
            }
        }

        public void DisplaySpellCard()
        {
            try
            {
                // ʹ��Resources.Load��������ȡResources�ļ�����ģ��
                // Ŀǰʹ�ÿ���ֱ�Ӷ�ȡ����������Դ��ʽ
                // TODO
                string imgPath = "ArtAssets/Card/ħ����/" + card.cardName;

                float imageSizeScale = 1f;

                Sprite sprite = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
                cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
                cardImage.overrideSprite = sprite;
            }
            catch
            {
                Debug.Log(card.cardName + " ��ģ��");
            }
        }

        /// <summary>
        /// ��ʾ���ƣ�������������UI��
        /// </summary>
        public void DisplayCard()
        {
            // Ĭ�Ϲر���ֵ��
            UnitCanvas.gameObject.SetActive(false);

            // ���ؿ���&����
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