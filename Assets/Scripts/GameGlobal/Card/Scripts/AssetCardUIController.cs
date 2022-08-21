using BattleSystem.Controller.Unit;
using DataScripts.Card;
using DataScripts.DataLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 卡牌显示，通过UnityEngine.UI修改卡牌模板
/// </summary>
public class AssetCardUIController : MonoBehaviour
{


    /// <summary>
    /// 基础卡牌信息
    /// </summary>
    public Text cardName;
    public Image cardImage;
    public RawImage raw;
    [SerializeField]
    public int CardNums;
    private int _cardNum;


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

    bool isChar;

    /// <summary>
    /// 悬浮显示相关
    /// </summary>
    private Vector3 _ObjectScale;
    private int _maxNum;

    void Start()
    {

       
        cardImage.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        raw.transform.localScale = new Vector3(0.7f, 0.7f, 1);

        _ObjectScale = gameObject.transform.localScale;
        if (card != null)
        {
            DisplayCard();
        }
        isChar = card.CardType == cfg.card.CardType.Chara;
        //  cardImage.SetNativeSize();
        raw.SetNativeSize();
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
        if (CardGroupManager.Instance.isConfig && eventData.button == PointerEventData.InputButton.Left)
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

        // Debug.Log(CardNums);


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

        if (!manager.SelectCard.ContainsKey(card.CardID))
        {
            manager.SelectCard.Add(card.CardID, 1);
            GameObject RightObject = null;
            RightObject = Instantiate(manager.prefabRight, manager.RightCards.transform.transform.GetChild(3));
            RightObject.name = card.CardID.ToString();

            GroupCardDisplay GCD = RightObject.GetComponent<GroupCardDisplay>(); ;
            GCD.cardStatus = CardStatus.Up;
            GCD.cardName.text = card.CardName;
            GCD.card = card;
            GCD.CardNums = 1;
            GCD.numText.text = "1";
            //RightObject.GetComponent<GroupCardDisplay>().CardNums = 1;
            Debug.Log(GCD.CardNums);
        }
        else
        {
            manager.SelectCard[card.CardID]++;
            GroupCardDisplay GCD = manager.RightCards.transform.GetChild(3).Find(card.CardID.ToString()).GetComponent<GroupCardDisplay>();
            //分类 manager.RightCards.transform.Find(UnitInfoDisplay.Instance.DIRECTORY[card.CardName]).Find(card.CardID.ToString()).GetComponent<GroupCardDisplay>();
            GCD.CardNums++;
            GCD.numText.text = GCD.CardNums.ToString();

        }





        CardNums--;
        if (!isChar) manager.AllCardNums++;
        else manager.CharNums++;
        numText.text = CardNums.ToString();

        manager.CurCardStage.text = manager.AllCardNums + "/" + manager.MaxCardNums;
        manager.CharCardStage.text = manager.CharNums + "/" + manager.MaxCharNums;

        CardGroupManager.Instance.StageCard[card.CardID]--;
    }
    private void Up2Down(GameObject gameObject)
    {


        manager.SelectCard[card.CardID]--;
        if (manager.SelectCard[card.CardID] == 0)
        {
            manager.SelectCard.Remove(card.CardID);
        }
        CardNums--;
        if (!isChar) manager.AllCardNums--;
        else manager.CharNums--;

        numText.text = CardNums.ToString();

        manager.CurCardStage.text = manager.AllCardNums + "/" + manager.MaxCardNums;
        manager.CharCardStage.text = manager.CharNums + "/" + manager.MaxCharNums; ;
        GameObject LeftObject = manager.LeftCards.transform.Find(UnitInfoDisplay.Instance.DIRECTORY[card.CardName]).Find(card.CardID.ToString()).gameObject;
        GameObject ObjectAll = manager.LeftCards.transform.GetChild(3).Find(card.CardID.ToString()).gameObject;
        GroupCardDisplay GCD = LeftObject.GetComponent<GroupCardDisplay>();
        GCD.CardNums++;
        GCD.numText.text = GCD.CardNums.ToString();
        GroupCardDisplay all = ObjectAll.GetComponent<GroupCardDisplay>();
        all.CardNums++;
        all.numText.text = all.CardNums.ToString();
        if (CardNums == 0) Destroy(this.gameObject);

        CardGroupManager.Instance.StageCard[card.CardID]++;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && canShow)
        {
            UnitInfoDisplay.Instance.GCDInit(this);
            UnitInfoDisplay.Instance.ReDraw_Card(this);
            //    if(this.card.CardType==cfg.card.CardType.Chara)UnitInfoDisplay.Instance.
        }
        // CardColorChange();
    }

    public void DisplayUnitCard(UnitCard unitCard)
    {
        atkText.text = unitCard.Atk.ToString();
        hpText.text = unitCard.Hp.ToString();
        UnitCanvas.gameObject.SetActive(true);
        DisplayCardImage();

        if (cardStatus == CardStatus.Down)
        {
            CardNums = UserLoader.Instance.cardInfo[unitCard.CardID];
            numText.text = CardNums.ToString();
        }
    }

    public void AddNumber(int n=1) {
        if (_cardNum <= 0 || _cardNum >= _maxNum) {
            return;
        }
        numText.text = (_cardNum+1).ToString();
    }

    public void ReduceNumber(int n = 1)
    {
        if (_cardNum <= 0 || _cardNum >= _maxNum)
        {
            return;
        }
        numText.text = (_cardNum - n).ToString();
    }

   
    public void DisplaySpellCard()
    {
        DisplayCardImage();
    }

    public async void DisplayCardImage()
    {
        try
        {
            const float imageSizeScale = 1.2f;

            Sprite sprite = await Addressables.LoadAssetAsync<Sprite>(card.CardName).Task;
            Texture texture = await Addressables.LoadAssetAsync<Texture>(card.CardName).Task;
            cardImage.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
            cardImage.overrideSprite = sprite;
            raw.rectTransform.sizeDelta = new Vector2(sprite.rect.width * imageSizeScale, sprite.rect.height * imageSizeScale);
            raw.texture = texture;
        }
        catch
        {
            Debug.Log(card.CardName + "无卡图");
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
        cardName.text = card.CardName;

        if (card is UnitCard)
        {
            var unitcard = card as UnitCard;
            DisplayUnitCard(unitcard);
        }

    }

    /// <summary>
    /// 显示卡牌：将卡牌数据与UI绑定
    /// </summary>
    public void Initialization(int id)
    {
        // 默认关闭数值表
        UnitCanvas.gameObject.SetActive(false);

        // 加载卡名&描述
        cardName.text = card.CardName;

        if (card is UnitCard)
        {
            var unitcard = card as UnitCard;
            DisplayUnitCard(unitcard);
        }

    }


}
