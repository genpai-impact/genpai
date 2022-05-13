using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using cfg.card;
using cfg;
namespace Genpai
{
    public enum InfoCardType
    {
        HandCardInfo,
        CharaCardInfo,
        SpellCardInfo,
        MonsterOnBattleInfo,
        CharaOnBattleInfo,
        BossInfo
    }
    //这个类写的跟米田共酱一样555
    public class UnitInfoDisplay : MonoSingleton<UnitInfoDisplay>//,IPointerClickHandler
    {

        public GameObject ParentText;
        public GameObject UnitPic;
        public GameObject BattleCardInfo;
        public GameObject SpellCardInfo;
        public GameObject GroupCardInfo;
        private Dictionary<CardType, string> TYPE = new Dictionary<CardType, string>();
        private Dictionary<ElementEnum, string> ELEM = new Dictionary<ElementEnum, string>();

        public Image TypeImage;
        public readonly Dictionary<string, string> DIRECTORY = new Dictionary<string, string>();
        public GameObject attachManager;//附着管理
      
        private UnitView unitView;
        private GroupCardDisplay GCD;
        private const string picPath = "ArtAssets/UI/战斗界面/二级菜单/上面的图片/单位图片/";
        private const string typePath = "ArtAssets/UI/战斗界面/新版战斗界面";
        private const string NormalElePath = "ArtAssets/UI/战斗界面/人物Buff";
        private const string skillImgPath = "ArtAssets/UI/战斗界面/二级菜单/Boss技能";
        private const string CardPath = "ArtAssets/Card/魔法牌";
        Vector3 hidePos;//隐藏坐标
        Vector3 showPos;//展示坐标
        public Vector3 curPos;//当前坐标
        public float curAlpha;//当前alpha
        [SerializeField]
        private HorizontalLayoutGroup PasLayout;
        public bool isShow = false;

        public bool isHide = false;

        public bool moveFlag;

        public float slideTime;
        public GameObject EmptyArea;

        public Text HPText;
        public Text NameText;
       public Text ATKText ;
       public Text PowText;


        /*****************************/
        public GameObject TagManager;//tag管理
        private GameObject curState;//当前状态
        private GameObject ProSkiTag;//主动技能
        private GameObject PasSkiTag;//被动技能
        /**************************/

        public enum state
        {
            show, hide
        }
        public state STATE;
        public void Awake()
        {
            
            curState = TagManager.transform.GetChild(0).GetChild(0).gameObject;
            ProSkiTag = TagManager.transform.GetChild(1).GetChild(0).gameObject;
            PasSkiTag = TagManager.transform.GetChild(2).GetChild(0).gameObject;

            hidePos = transform.localPosition;// new Vector3(1323, 0, 0);
            showPos = new Vector3(hidePos.x - 613, 0, 0);
            InitTransform();
            InitPicPath();
            STATE = state.hide;

            //Hide();
        }
        private void Start()
        {
            SkillLoader.MySkillLoad();
        }
        private void Update()
        {
            // if(ShowCard!=null) Debug.Log(ShowCard.name);
            if (slideTime > 0.5f)
            {
                slideTime = 0;
                isShow = false;
                isHide = false;
            }
            if (isShow)
            {  
                isHide = false;
                slideTime += Time.deltaTime;
                this.transform.localPosition = Vector3.Lerp(hidePos, showPos, slideTime / 0.5f);
                GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, slideTime / 0.5f);
                STATE = state.show;
            }
            if (isHide)
            {
                isShow = false;
                slideTime += Time.deltaTime;
                this.transform.localPosition = Vector3.Lerp(curPos, hidePos, slideTime / 0.5f);
                GetComponent<CanvasGroup>().alpha = Mathf.Lerp(curAlpha, 0, slideTime / 0.5f);
            }
        }


        private void InitTransform()
        {
            TYPE.Add(CardType.Monster, "怪物卡");
            TYPE.Add(CardType.Chara, "角色卡");
            TYPE.Add(CardType.Boss, "BOSS卡");

            ELEM.Add(ElementEnum.None, "无");
            ELEM.Add(ElementEnum.Pyro, "火");
            ELEM.Add(ElementEnum.Hydro, "水");
            ELEM.Add(ElementEnum.Cryo, "冰");
            ELEM.Add(ElementEnum.Electro, "雷");
            ELEM.Add(ElementEnum.Anemo, "风");
            ELEM.Add(ElementEnum.Geo, "岩");
        }
        public void refleshEle(UnitView unit)
        {
            if (unit.SelfElement != ElementEnum.None)
            {
                attachManager.transform.GetChild(0).gameObject.SetActive(true);
                attachManager.transform.GetChild(0).GetComponent<Image>().sprite =
                    Resources.Load(NormalElePath + "/" + "人物元素Buff-" + unit.SelfElement.ToString(), typeof(Sprite)) as Sprite;
            }
            else attachManager.transform.GetChild(0).gameObject.SetActive(false);
        }
        private void InitPicPath()
        {
            DIRECTORY.Add("丘丘人", "丘丘人");
            DIRECTORY.Add("打手丘丘人", "丘丘人");

            //加入新牌时在这里加入，或者直接读取卡牌文件？

            {/*Hilichurl.Add("冰箭丘丘人", "丘丘人");
            Hilichurl.Add("冲锋丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");*/
            }

            DIRECTORY.Add("史莱姆·水", "史莱姆");
            DIRECTORY.Add("史莱姆·火", "史莱姆");
            DIRECTORY.Add("史莱姆·冰", "史莱姆");
            DIRECTORY.Add("史莱姆·雷", "史莱姆");
            DIRECTORY.Add("史莱姆·风", "史莱姆");
            DIRECTORY.Add("史莱姆·岩", "史莱姆");
            //DIRECTORY.Add("史莱姆·岩", "史莱姆");
            //Slime.Add("史莱姆·草", "史莱姆");

            DIRECTORY.Add("刻晴", "角色");
            DIRECTORY.Add("芭芭拉", "角色");
            DIRECTORY.Add("霜铠丘丘王", "Boss");
            //DIRECTORY.Add("胡桃", "角色");

        }
        private void refleshCurSta(UnitView unit)//更新状态tag
        {
            //Debug.Log(curState.name);   
            curState.transform.parent.GetComponent<Text>().text = "当前状态";
            GameObject curImage = curState.transform.GetChild(0).GetChild(0).gameObject;
            GameObject curDescribe = curImage.transform.GetChild(0).gameObject;
            curImage.GetComponent<Image>().sprite =
                Resources.Load(NormalElePath + "/" + "人物元素Buff-" + unit.SelfElement.ToString(), typeof(Sprite)) as Sprite;
            curDescribe.GetComponent<Text>().text = ELEM[unit.SelfElement] + "元素附着";
        }
        private void refleshCurSta(GroupCardDisplay GCD)//更新背景故事
        {
            //Debug.LogError(curState.name);
            curState.transform.parent.GetComponent<Text>().text = "背景故事";
            GameObject backGround = curState.transform.GetChild(3).gameObject;
            //GameObject Attach = curState.transform.GetChild(0).gameObject;
            //GameObject buff = curState.transform.GetChild(1).gameObject;
            //GameObject debuff = curState.transform.GetChild(2).gameObject;
            //Attach.SetActive(false);
            //buff.SetActive(false);
            //debuff.SetActive(false);
           // Debug.Log(GCD.card.cardInfo.Length);
            backGround.GetComponent<Text>().text = GCD.card.cardInfo[0];
        }
        private void refleshBuff(UnitView unit)//更新buff
        {
            GameObject buff = curState.transform.GetChild(1).GetChild(0).gameObject;
            GameObject buffName = buff.transform.GetChild(0).gameObject;
            GameObject Describe = buff.transform.GetChild(1).gameObject;
            int cnt = unit.BuffViews.Count;
            if (cnt == 0)
            {
                buff.GetComponent<Image>().sprite = Resources.Load(NormalElePath + "/" + "万能临时buff", typeof(Sprite)) as Sprite;
                buffName.GetComponent<Text>().text = "无";
                Describe.GetComponent<Text>().text = "无Buff";
            }
        }
        private void refleshDebuff(UnitView unit)//更新debff
        {
            GameObject debuff = curState.transform.GetChild(2).GetChild(0).gameObject;
            GameObject debuffName = debuff.transform.GetChild(0).gameObject;
            GameObject Describe = debuff.transform.GetChild(1).gameObject;
            int cnt = unit.BuffViews.Count;
            if (cnt == 0)
            {
                debuff.GetComponent<Image>().sprite = Resources.Load(NormalElePath + "/" + "万能临时buff", typeof(Sprite)) as Sprite;
                debuffName.GetComponent<Text>().text = "无";
                Describe.GetComponent<Text>().text = "无Debuff";
            }
        }
       
        private void refleshProSkill(UnitView unitView)
        {
            if (unitView.UnitType == CardType.Monster)
            {
                ProSkiTag.transform.parent.gameObject.SetActive(false);
                return;
            }
            ProSkiTag.transform.parent.gameObject.SetActive(true);
            List<SkillLoader.SkillData> SkillList = getSkillList(unitView, SkillType.Erupt);
            //  Debug.Log(SkillList.Count);
            CardType type = unitView.UnitType;
            GameObject curSkill = ProSkiTag.transform.GetChild((int)type).gameObject;//获取unit对应的技能节点
            for (int i = 0; i < ProSkiTag.transform.childCount; i++)//将不属于节点类型的技能页隐藏
            {
                if (i == (int)type) ProSkiTag.transform.GetChild(i).gameObject.SetActive(true);
                else ProSkiTag.transform.GetChild(i).gameObject.SetActive(false);
            }
            GameObject FirstSkill = curSkill.transform.GetChild(0).gameObject;
            GameObject SecondSkill = null;
            if (SkillList.Count == 2)
            {
                SecondSkill = curSkill.transform.GetChild(1).gameObject;
                SecondSkill.SetActive(true);
                SkillUpdate(FirstSkill, SecondSkill, SkillList);
            }
            else SkillUpdate(FirstSkill, SkillList);
        }
        private void refleshProSkill(GroupCardDisplay GCD)//更新卡组二级菜单人物技能
        {
            if (GCD.card.cardType == cfg.card.CardType.Monster)
            {
                ProSkiTag.transform.parent.gameObject.SetActive(false);
                return;
            }
            ProSkiTag.transform.parent.gameObject.SetActive(true);
            List<SkillLoader.SkillData> SkillList = getSkillList(GCD, SkillType.Erupt);
            //  Debug.Log(SkillList.Count);
            cfg.card.CardType type = GCD.card.cardType;
          //  Debug.Log((int)type);
            GameObject curSkill = ProSkiTag.transform.GetChild((int)type).gameObject;//获取unit对应的技能节点
           // Debug.Log(curSkill.name);
            for (int i = 0; i < ProSkiTag.transform.childCount; i++)//将不属于节点类型的技能页隐藏
            {
                if (i == (int)type) ProSkiTag.transform.GetChild(i).gameObject.SetActive(true);
                else ProSkiTag.transform.GetChild(i).gameObject.SetActive(false);
            }
           // Debug.Log("okok");
            GameObject FirstSkill = curSkill.transform.GetChild(0).gameObject;
          //  Debug.Log(FirstSkill.name);
            GameObject SecondSkill = null;
            if (SkillList.Count == 2)
            {
                SecondSkill = curSkill.transform.GetChild(1).gameObject;
                SecondSkill.SetActive(true);
                SkillUpdate(FirstSkill, SecondSkill, SkillList);
            }
            else SkillUpdate(FirstSkill, SkillList);
        }
        //更新指定目标的技能列表（双技能
        private void SkillUpdate(GameObject firstSkill, GameObject secondSkill, List<SkillLoader.SkillData> SkillList)
        {
           // Debug.Log(firstSkill.name+" "+secondSkill.name);
            string name = unitView != null ? unitView.UnitName : GCD.card.cardName;
            firstSkill.transform.GetChild(1).GetComponent<Text>().text = SkillList[0].SkillName;
            firstSkill.transform.GetChild(2).GetComponent<Text>().text = SkillList[0].SkillDesc;
            firstSkill.transform.GetChild(0).GetComponent<Image>().sprite =
                    Resources.Load(skillImgPath + "/" + name + "1", typeof(Sprite)) as Sprite;
            firstSkill.transform.GetChild(3).GetChild(0).GetComponent<Text>().text =
               "CD" + SkillLoader.HitomiSkillDataList[name][0].Cost.ToString();//暂时没有CD数据 用COST顶上

            secondSkill.transform.GetChild(0).GetComponent<Image>().sprite =
             Resources.Load(skillImgPath + "/" + name + "2", typeof(Sprite)) as Sprite;
            secondSkill.transform.GetChild(1).GetComponent<Text>().text = SkillList[1].SkillName;
            secondSkill.transform.GetChild(2).GetComponent<Text>().text = SkillList[1].SkillDesc;
            secondSkill.transform.GetChild(3).GetChild(0).GetComponent<Text>().text =
               "CD" + SkillLoader.HitomiSkillDataList[name][0].Cost.ToString();//暂时没有CD数据 用COST顶上
        }
        //更新指定目标的技能列表（单技能
        private void SkillUpdate(GameObject firstSkill, List<SkillLoader.SkillData> SkillList)
        {
            //Debug.Log(SkillList.Count);
            string name = unitView != null ? unitView.UnitName : GCD.card.cardName;
                firstSkill.transform.GetChild(0).GetComponent<Image>().sprite =
                      Resources.Load(skillImgPath + "/" + name + "1", typeof(Sprite)) as Sprite;
            firstSkill.transform.GetChild(1).GetComponent<Text>().text = SkillList[0].SkillName;
            firstSkill.transform.GetChild(2).GetComponent<Text>().text = SkillList[0].SkillDesc;
            //firstSkill.transform.GetChild(3).GetChild(0).GetComponent<Text>().text =
            //   "CD" + SkillLoader.HitomiSkillDataList[unitView.unitName][0].Cost.ToString();//暂时没有CD数据 用COST顶上
        }
        private void refleshPasSkill(UnitView unit)
        {
            if (unit.UnitType == CardType.Monster)
            {
                float offset = TagManager.GetComponent<RectTransform>().rect.width / TagManager.transform.childCount;
                //Debug.Log(PasSkiTag.GetComponent<RectTransform>().);
                if (!moveFlag)
                {
                    PasSkiTag.GetComponent<RectTransform>().anchoredPosition += new Vector2(offset, 0);
                    moveFlag = true;
                }
                // PasSkiTag.GetComponent<RectTransform>().transform.position
            }
            CardType type = unit.UnitType;
            List<SkillLoader.SkillData> SkillList = new List<SkillLoader.SkillData>();
            Debug.Log(unit.UnitName);
            if (unit.UnitType != CardType.Chara) SkillList = getSkillList(unit, SkillType.Passive);
            else SkillList = getSkillList(unit, SkillType.Coming);
            GameObject curSkill = PasSkiTag.transform.GetChild((int)type).gameObject;

            for (int i = 0; i < PasSkiTag.transform.childCount; i++)
            {
                if (i == (int)type) PasSkiTag.transform.GetChild(i).gameObject.SetActive(true);
                else PasSkiTag.transform.GetChild(i).gameObject.SetActive(false);
            }

            GameObject FirstSkill = curSkill.transform.GetChild(0).gameObject;
            GameObject SecondSkill = null;
            if (SkillList.Count == 2)
            {
                SecondSkill = curSkill.transform.GetChild(1).gameObject;
                SecondSkill.SetActive(true);
                SkillUpdate(FirstSkill, SecondSkill, SkillList);
            }
            else SkillUpdate(FirstSkill, SkillList);


        }
        private void refleshPasSkill(GroupCardDisplay GCD)
        {
            if (GCD.card.cardType == cfg.card.CardType.Monster)
            {
                float offset = TagManager.GetComponent<RectTransform>().rect.width / TagManager.transform.childCount;
                //Debug.Log(PasSkiTag.GetComponent<RectTransform>().);
                if (!moveFlag)
                {
                    PasSkiTag.GetComponent<RectTransform>().anchoredPosition += new Vector2(offset, 0);
                    moveFlag = true;
                }
                // PasSkiTag.GetComponent<RectTransform>().transform.position
            }
            else if(GCD.card.cardType == cfg.card.CardType.Chara)
            {
                UnitInfoCanva.Instance.PasSkill.anchoredPosition = UnitInfoCanva.Instance.PasOriginPos;
                moveFlag = false;
            }
            CardType type = (CardType)GCD.card.cardType;
            List<SkillLoader.SkillData> SkillList = new List<SkillLoader.SkillData>();
           // Debug.Log(GCD.card.cardName);
            
            if ((int)GCD.card.cardType != (int)CardType.Chara) SkillList = getSkillList(GCD, SkillType.Passive);
            else SkillList = getSkillList(GCD, SkillType.Coming);
            //Debug.Log(SkillList.Count);
            GameObject curSkill = PasSkiTag.transform.GetChild((int)type).gameObject;

            for (int i = 0; i < PasSkiTag.transform.childCount; i++)
            {
                if (i == (int)type) PasSkiTag.transform.GetChild(i).gameObject.SetActive(true);
                else PasSkiTag.transform.GetChild(i).gameObject.SetActive(false);
            }

            GameObject FirstSkill = curSkill.transform.GetChild(0).gameObject;
            GameObject SecondSkill = null;
            if (SkillList.Count == 2)
            {
                SecondSkill = curSkill.transform.GetChild(1).gameObject;
                SecondSkill.SetActive(true);
                SkillUpdate(FirstSkill, SecondSkill, SkillList);
            }
            else SkillUpdate(FirstSkill, SkillList);


        }
        private void InitImageType()
        {
            //imageType.Add("怪物卡",)
        }



        public void Init(UnitView _unit)
        {
            unitView = _unit;
        }
        public void GCDInit(GroupCardDisplay _gcd)
        {
            GCD = _gcd;
        }

        public UnitView GetUnit()
        {
            return unitView;
        }

        public void Display()
        {
            EmptyArea.SetActive(true);
            isShow = true;
           // Debug.Log("sss");
            if (unitView == null)
            {
                Debug.LogError("未初始化");
            }
            ReDraw();

            gameObject.SetActive(true);

        }
        /// <summary>
        /// 读取对应技能类型的所有技能
        /// </summary>
        List<SkillLoader.SkillData> getSkillList(UnitView unit, SkillType skillType)
        {
            List<SkillLoader.SkillData> skillList = new List<SkillLoader.SkillData>();
            if (SkillLoader.HitomiSkillDataList.ContainsKey(unit.UnitName))
            {
                // Debug.Log("包含  "+ SkillLoader.HitomiSkillDataList[unit.unitName].Count);
                for (int i = 0; i < SkillLoader.HitomiSkillDataList[unit.UnitName].Count; i++)
                {
                    //                    Debug.Log(SkillLoader.HitomiSkillDataList[unit.unitName].Count);
                    if (SkillLoader.HitomiSkillDataList[unit.UnitName][i].SkillType == skillType)
                        skillList.Add(SkillLoader.HitomiSkillDataList[unit.UnitName][i]);//加入符合类型的列表
                }
            }
            else throw new System.Exception("找不到对应技能");
            return skillList;
        }
        List<SkillLoader.SkillData> getSkillList(GroupCardDisplay GCD, SkillType skillType)
        {
            List<SkillLoader.SkillData> skillList = new List<SkillLoader.SkillData>();
            if (SkillLoader.HitomiSkillDataList.ContainsKey(GCD.card.cardName))
            {
              //   Debug.Log("包含  "+ SkillLoader.HitomiSkillDataList[GCD.card.cardName].Count);
                for (int i = 0; i < SkillLoader.HitomiSkillDataList[GCD.card.cardName].Count; i++)
                {
                    //                    Debug.Log(SkillLoader.HitomiSkillDataList[unit.unitName].Count);
                    if (SkillLoader.HitomiSkillDataList[GCD.card.cardName][i].SkillType == skillType)
                        skillList.Add(SkillLoader.HitomiSkillDataList[GCD.card.cardName][i]);//加入符合类型的列表
                }
            }
            else throw new System.Exception("找不到对应技能");
            return skillList;
        }
        private void switchType(UnitView unit)
        {
            switch (unit.UnitType)
            {
                case CardType.Monster:
                    TypeImage.sprite = Resources.Load(typePath + "/" + "二级菜单-怪物", typeof(Sprite)) as Sprite;
                    break;
                case CardType.Chara:
                    TypeImage.sprite = Resources.Load(typePath + "/二级菜单-角色", typeof(Sprite)) as Sprite;
                    break;
                case CardType.Boss:
                    TypeImage.sprite = Resources.Load(typePath + "/二级菜单-boss", typeof(Sprite)) as Sprite;
                    break;
            }
        }

        private void switchType(CardDisplay card)
        {
            if (card.Card.cardType == cfg.card.CardType.Spell)
                TypeImage.sprite = Resources.Load(typePath + "/" + "二级菜单-魔法卡", typeof(Sprite)) as Sprite;
            else TypeImage.sprite = Resources.Load(typePath + "/" + "二级菜单-怪物", typeof(Sprite)) as Sprite;
        }
        private void switchType(GroupCardDisplay card)
        {
            switch ((CardType)card.card.cardType)
            {
                case CardType.Monster:
                    TypeImage.sprite = Resources.Load(typePath + "/" + "二级菜单-怪物", typeof(Sprite)) as Sprite;
                    break;
                case CardType.Chara:
                    TypeImage.sprite = Resources.Load(typePath + "/二级菜单-角色", typeof(Sprite)) as Sprite;
                    break;
                case CardType.Boss:
                    TypeImage.sprite = Resources.Load(typePath + "/二级菜单-boss", typeof(Sprite)) as Sprite;
                    break;
            }
        }
        private void ReDraw()//_MonsterOnBattle()
        {
            BattleCardInfo.SetActive(true);
            SpellCardInfo.SetActive(false);
            HPText.text = "生命值:" + unitView.Hp.ToString();
            NameText.text = unitView.UnitName;
            //baseATK=ATK?
            ATKText.text = "攻击力:" + unitView.Atk.ToString();
            PowText.text = "能量:" + unitView.Mp.ToString();
            stateInit();
            switchType(unitView);
            refleshEle(unitView);
            refleshCurSta(unitView);
            refleshBuff(unitView);
            refleshDebuff(unitView);
            refleshProSkill(unitView);
            refleshPasSkill(unitView);
           // Debug.Log(unit.unitName);
            string path = picPath + DIRECTORY[unitView.UnitName] + "/" + unitView.UnitName; 

            Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            UnitPic.GetComponent<Image>().sprite = sprite;

        }
        void stateInit()
        {
            curState.GetComponent<CanvasGroup>().alpha = 1;
            PasSkiTag.GetComponent<CanvasGroup>().alpha = 0;
            ProSkiTag.GetComponent<CanvasGroup>().alpha = 0;
        }
        public void ReDraw_Card(CardDisplay card)//战斗场景手牌二级菜单
        {
            if (EmptyArea.activeInHierarchy == false) isShow = true;
            EmptyArea.SetActive(true);
            BattleCardInfo.SetActive(false);
            SpellCardInfo.SetActive(true);
            //  Debug.Log(card.card.cardType);
            Sprite sprite = null;
            string path = null;
            switch (card.Card.cardType)
            {
                case cfg.card.CardType.Spell:
                    SpellCardInfo.transform.GetChild(1).gameObject.SetActive(false);
                    path = CardPath + "/" + card.cardName.text;
                    transform.Find("Name").GetComponent<Text>().text = card.Card.cardName;
                    SpellCardInfo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = card.Card.cardName;
                    SpellCardInfo.transform.GetChild(0).GetChild(2).GetComponent<Text>().text =
                        SpellCardLoader.Instance.SpellCardDataDic[SpellCardLoader.Instance.SpellName[card.cardName.text]].CardInfo;
                    //Debug.Log("卡名" + card.cardName.text);
                    break;
                case cfg.card.CardType.Monster:

                    SpellCardInfo.transform.GetChild(1).gameObject.SetActive(true);
                    transform.Find("Name").GetComponent<Text>().text = card.Card.cardName;
                    List<SkillLoader.SkillData> skillList = SkillLoader.HitomiSkillDataList[card.Card.cardName];
                    SpellCardInfo.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = skillList[0].SkillName;
                    SpellCardInfo.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = skillList[0].SkillDesc;
                    SpellCardInfo.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = skillList[1].SkillName;
                    SpellCardInfo.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = skillList[1].SkillDesc;

                    path = picPath + DIRECTORY[card.cardName.text] + "/" + card.cardName.text;
                    break;
            }

            
            sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            UnitPic.GetComponent<Image>().sprite = sprite;
            switchType(card);
        }
        public void ReDraw_Card(GroupCardDisplay card)//更新卡组界面二级菜单
        {
           // Debug.Log("redraw");    
            if (EmptyArea.activeInHierarchy == false) isShow = true;
            //Invoke("deleyDraw", 0.1f);
            EmptyArea.SetActive(true);
            BattleCardInfo.SetActive(true);
            if(card.card is CharaCard)
            {
                PowText.gameObject.SetActive(true);
                HPText.text = "生命值:" + (card.card as CharaCard).hp.ToString();
             ATKText.text = "攻击力:" + (card.card as CharaCard).atk.ToString();
             PowText.text = "能量:" + (card.card as CharaCard).MAXMP.ToString();
            }
            else
            {
                HPText.text = "生命值:" + (card.card as UnitCard).hp.ToString();
                ATKText.text = "攻击力:" + (card.card as UnitCard).atk.ToString();
                PowText.gameObject.SetActive(false);
            }
           
            NameText.text = card.card.cardName.ToString();
          
            Sprite sprite = null;
            string path = null;
            // Debug.Log(unitView.unitName);
            path = picPath + DIRECTORY[card.cardName.text] + "/" + card.cardName.text;
            refleshProSkill(card);
            refleshPasSkill(card);
            refleshCurSta(card);
            stateInit();

            sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            UnitPic.GetComponent<Image>().sprite = sprite;
            switchType(card);
        }
        void deleyDraw()
        {
            EmptyArea.SetActive(true);
        }

        private void OnDisable()
        {
            SkillLoader.Clean();
        }

    }

}