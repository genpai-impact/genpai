using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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

    public class UnitInfoDisplay : MonoBehaviour
    {

        public GameObject ParentText;
        public GameObject UnitPic;
        private Dictionary<UnitType, string> TYPE = new Dictionary<UnitType, string>();
        private Dictionary<ElementEnum, string> ELEM = new Dictionary<ElementEnum, string>();

        private Dictionary<string, string> DIRECTORY = new Dictionary<string, string>();

        private UnitView unit;
        private const string picPath = "ArtAssets/UI/战斗界面/二级菜单/上面的图片/单位图片/";

        public void Start()
        {
            InitTransform();
            InitPicPath();

            Hide();
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
            //Slime.Add("史莱姆·草", "史莱姆");

            DIRECTORY.Add("刻晴", "角色");
            DIRECTORY.Add("芭芭拉", "角色");
            //DIRECTORY.Add("胡桃", "角色");

        }


        public void Init(UnitView _unit)
        {
            unit = _unit;
        }


        public UnitView GetUnitEntity()
        {
            return unit;
        }

        public void Display(InfoCardType _type)
        {
            //检查是否初始化
            if (unit == null)
            {
                Debug.LogError("未初始化");
            }

            switch (_type)
            {
                case InfoCardType.MonsterOnBattleInfo:
                    ReDraw_MonsterOnBattle();
                    break;
                default:
                    Debug.Log("can not find this Infotype");
                    break;
            }

            if (_type == InfoCardType.MonsterOnBattleInfo)
                ReDraw_MonsterOnBattle();

            gameObject.SetActive(true);
        }

        private void ReDraw_MonsterOnBattle()
        {
            Text HPText = ParentText.transform.Find("HP").GetComponent<Text>();
            Text NameText = ParentText.transform.Find("Name").GetComponent<Text>();
            Text ATKText = ParentText.transform.Find("ATK").GetComponent<Text>();
            Text FeatureText = ParentText.transform.Find("Feature").GetComponent<Text>();
            Text AttrText = ParentText.transform.Find("Attribute").GetComponent<Text>();
            Text InfoText = ParentText.transform.Find("Info").GetComponent<Text>();

            HPText.text = unit.HP.ToString();
            NameText.text = unit.unitName;
            //baseATK=ATK?
            ATKText.text = unit.ATK.ToString();
            //卡牌特性，目前没有，暂定为固定语句
            FeatureText.text = "【战吼】：可爱值提升至上限";
            // TODO：添加本真属性
            AttrText.text = TYPE[unit.unitType] + "/" + ELEM[unit.ATKElement] + "属性";

            //TODO:BUFF_list
            StringBuilder InfoBuilder = new StringBuilder();
            InfoBuilder.Append("<size=24> 状态：</size>\n");
            InfoBuilder.Append("<size=22> 当前生命值：" + unit.HP + " </size>\n");
            InfoBuilder.Append("<size=22> 当前攻击力：" + unit.ATK + " </size>\n");
            InfoBuilder.Append("<size=22> 元素附着：" + ELEM[unit.SelfElement] + "元素 </size>\n");
            InfoBuilder.Append("<size=22> BUFF：无 </size>");
            InfoText.text = InfoBuilder.ToString();

            string path = picPath + DIRECTORY[unit.unitName] + "/" + unit.unitName;

            Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            UnitPic.GetComponent<Image>().sprite = sprite;

        }

        private void ReDraw_HandCard() { }

        private void ReDraw_CharaCard() { }

        private void ReDraw_SpellCard() { }

        private void ReDraw_CharaOnBattle() { }

        private void ReDraw_Boss() { }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}