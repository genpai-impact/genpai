using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    public class UnitInfoDisplay : MonoBehaviour//,IPointerClickHandler
    {

        public GameObject ParentText;
        public GameObject UnitPic;
        private Dictionary<UnitType, string> TYPE = new Dictionary<UnitType, string>();
        private Dictionary<ElementEnum, string> ELEM = new Dictionary<ElementEnum, string>();
      
        public Image TypeImage;
        private Dictionary<string, string> DIRECTORY = new Dictionary<string, string>();
        public GameObject attachManager;//附着管理
      
        private UnitView unit;

        private const string picPath = "ArtAssets/UI/战斗界面/二级菜单/上面的图片/单位图片/";
        private const string typePath = "ArtAssets/UI/战斗界面/新版战斗界面";
        private const string NormalElePath = "ArtAssets/UI/战斗界面/人物Buff";

        Vector3 hidePos;//隐藏坐标
        Vector3 showPos;//展示坐标
        public Vector3 curPos;//当前坐标
        public float curAlpha;//当前alpha
       
        public bool isShow = false;
   
        public bool isHide = false;

        public float slideTime;
        public GameObject EmptyArea;

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
        public void Start()
        {
            curState = TagManager.transform.GetChild(0).GetChild(0).gameObject;
            ProSkiTag = TagManager.transform.GetChild(1).gameObject;
            PasSkiTag = TagManager.transform.GetChild(2).gameObject;

            hidePos = transform.localPosition;// new Vector3(1323, 0, 0);
            showPos = new Vector3(hidePos.x - 613, 0, 0);
            InitTransform();
            InitPicPath();
            STATE = state.hide;

            //Hide();
        }
        private void Update()
        {
            if (slideTime > 0.5f)
            {
                slideTime = 0;
                isShow = false;
                isHide = false;
            }
            if (isShow)
            {  // if(unit.)
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
            TYPE.Add(UnitType.Monster, "怪物卡");
            TYPE.Add(UnitType.Chara, "角色卡");
            TYPE.Add(UnitType.Boss, "BOSS卡");

            ELEM.Add(ElementEnum.None, "无");
            ELEM.Add(ElementEnum.Pyro, "火");
            ELEM.Add(ElementEnum.Hydro, "水");
            ELEM.Add(ElementEnum.Cryo, "冰");
            ELEM.Add(ElementEnum.Electro, "雷");
            ELEM.Add(ElementEnum.Anemo, "风");
            ELEM.Add(ElementEnum.Geo, "岩");
        }
        public void refleshBuff(UnitView unit)
        {
            if(unit.SelfElement!=ElementEnum.None)
            {
                attachManager.transform.GetChild(0).gameObject.SetActive(true);
                attachManager.transform.GetChild(0).GetComponent<Image>().sprite =
                    Resources.Load(NormalElePath + "/" + "人物元素Buff-"+ unit.SelfElement.ToString(), typeof(Sprite)) as Sprite;
            }
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
            DIRECTORY.Add("Boss", "Boss");
            //DIRECTORY.Add("胡桃", "角色");

        }
        private void refleshCurSta(UnitView unit)//更新状态tag
        {
            GameObject curImage = curState.transform.GetChild(0).GetChild(0).gameObject;
            Debug.Log(curImage.name);
            GameObject curDescribe = curImage.transform.GetChild(0).gameObject;
            curImage.GetComponent<Image>().sprite = 
                Resources.Load(NormalElePath + "/" + "人物元素Buff-" + unit.SelfElement.ToString(), typeof(Sprite)) as Sprite;
            curDescribe.GetComponent<Text>().text = ELEM[unit.SelfElement] + "元素附着";
            Debug.Log(curImage.name);
        }
        private void refleshProSki(UnitView unit)//更新主动tag
        {
            GameObject buff = curState.transform.GetChild(1).GetChild(0).gameObject;
            GameObject buffName= buff.transform.GetChild(0).gameObject;
            GameObject Describe= buff.transform.GetChild(1).gameObject;
            int cnt = unit.buffViews.Count;
            if(cnt==0)
            {
                buff.GetComponent<Image>().sprite= Resources.Load(NormalElePath + "/" + "万能临时buff", typeof(Sprite)) as Sprite;
                buffName.GetComponent<Text>().text = "无";
                Describe.GetComponent<Text>().text = "无Buff";
            }
        }
        private void refleshPasSki(UnitView unit)//更新被动tag
        {
            GameObject debuff = curState.transform.GetChild(2).GetChild(0).gameObject;
            GameObject debuffName = debuff.transform.GetChild(0).gameObject;
            GameObject Describe = debuff.transform.GetChild(1).gameObject;
            int cnt = unit.buffViews.Count;
            if (cnt == 0)
            {
               debuff.GetComponent<Image>().sprite = Resources.Load(NormalElePath + "/" + "万能临时buff", typeof(Sprite)) as Sprite;
                debuffName.GetComponent<Text>().text = "无";
                Describe.GetComponent<Text>().text = "无Debuff";
            }
        }

        private void InitImageType()
            {
            //imageType.Add("怪物卡",)
            }
       
        
     
        public void Init(UnitView _unit)
        {
            unit = _unit;
        }


        public UnitView GetUnit()
        {
            return unit;
        }
       
        public void Display(InfoCardType _type)
        {
//<<<<<<< HEAD
            EmptyArea.SetActive(true);
            Debug.Log("show");
            isShow = true;
//=======
//>>>>>>> 03e1dd2d6de7e22c4736090c98fbcc5ddd012003
            //检查是否初始化
            if (unit == null)
            {
                Debug.LogError("未初始化");
            }

            switch (_type)
            {
                case InfoCardType.MonsterOnBattleInfo:
                    ReDraw();// _MonsterOnBattle();
                    break;
                default:
                    Debug.Log("can not find this Infotype");
                    break;
            }
            
            //if (_type == InfoCardType.MonsterOnBattleInfo)
            //    ReDraw_MonsterOnBattle();

            gameObject.SetActive(true);
           
        }
        private void switchType(UnitView unit)
        {
            switch(unit.unitType)
            {
                case UnitType.Monster:
                    TypeImage.sprite = Resources.Load(typePath + "/"+"二级菜单-怪物", typeof(Sprite))as Sprite;
                    break;
                case UnitType.Chara:
                    TypeImage.sprite = Resources.Load(typePath + "/二级菜单-角色", typeof(Sprite)) as Sprite;
                    break;
                case UnitType.Boss:
                    TypeImage.sprite = Resources.Load(typePath + "/二级菜单-boss", typeof(Sprite)) as Sprite;
                    break;
            }
        }
        private void ReDraw()//_MonsterOnBattle()
        {


            Text HPText = ParentText.transform.Find("HP").GetComponent<Text>();
            Text NameText = ParentText.transform.parent.Find("Name").GetComponent<Text>();
            Text ATKText = ParentText.transform.Find("ATK").GetComponent<Text>();
            Text PowText = ParentText.transform.Find("POW").GetComponent<Text>();

            //Text FeatureText = ParentText.transform.Find("Feature").GetComponent<Text>();
            //Text AttrText = ParentText.transform.Find("Attribute").GetComponent<Text>();
            //Text InfoText = ParentText.transform.Find("Info").GetComponent<Text>();
           

            HPText.text = "生命值:"+unit.HP.ToString();
            NameText.text = unit.unitName;
            //baseATK=ATK?
            ATKText.text = "攻击力:"+unit.ATK.ToString();
            PowText.text = "能量:"+unit.MP.ToString();
            //TypeImage.texture=
            //卡牌特性，目前没有，暂定为固定语句
            //  FeatureText.text = "【战吼】：可爱值提升至上限";
            // TODO：添加固有属性
            //  AttrText.text = TYPE[unit.unitType] + "/" + ELEM[unit.ATKElement] + "属性";

            //StringBuilder InfoBuilder = new StringBuilder();
            //InfoBuilder.Append("<size=24> 状态：</size>\n");
            //InfoBuilder.Append("<size=22> 当前生命值：" + unit.HP + " </size>\n");
            //InfoBuilder.Append("<size=22> 当前攻击力：" + unit.ATK + " </size>\n");
            //InfoBuilder.Append("<size=22> 元素附着：" + ELEM[unit.SelfElement] + "元素 </size>\n");

            //// Fixme：不知道为啥没作用
            //InfoBuilder.Append("<size=22> BUFF：</size>\n");
            //foreach (var buff in unit.buffViews)
            //{
            //    InfoBuilder.Append("<size=22> " + buff.ReturnDescription() + "</size>\n");
            //}

            //InfoText.text = InfoBuilder.ToString();
            switchType(unit);
            refleshBuff(unit);
            refleshCurSta(unit);
            refleshProSki(unit);
            refleshPasSki(unit);
            Debug.Log(unit.unitName);
            string path = picPath + DIRECTORY[unit.unitName] + "/" + unit.unitName;

            Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            UnitPic.GetComponent<Image>().sprite = sprite;

        }

        private void ReDraw_HandCard() { }

        private void ReDraw_CharaCard() { }

        private void ReDraw_SpellCard() { }

        private void ReDraw_CharaOnBattle() { }

        private void ReDraw_Boss() { }

        //public void Hide()
        //{
        //    isShow = false;
        //    curPos = transform.localPosition;
        //    curAlpha = this.transform.GetChild(0).GetComponent<CanvasGroup>().alpha;
        //    Debug.Log("hide");
        //    isHide = true;
        //    slideTime = 0;
            
            
        //  //  gameObject.SetActive(false);
        //}
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawLine(Camera.main.gameObject.transform.position, thisHit);
        //}

    }
}