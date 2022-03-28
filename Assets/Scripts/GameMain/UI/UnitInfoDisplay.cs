﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    public class UnitInfoDisplay : MonoBehaviour
    {

        public GameObject ParentText;
        public GameObject UnitPic;
        private Dictionary<UnitType, string> TYPE = new Dictionary<UnitType, string>();
        private Dictionary<ElementEnum, string> ELEM = new Dictionary<ElementEnum, string>();

        private Dictionary<string ,string> Directory = new Dictionary<string, string>();

        private UnitEntity unitEntity;
        private Unit unit;
        private const string picPath= "ArtAssets/UI/战斗界面/二级菜单/上面的图片/单位图片/";

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
            Directory.Add("丘丘人", "丘丘人");
            Directory.Add("打手丘丘人", "丘丘人");

            //加入新牌时在这里加入，或者直接读取卡牌文件？

            {/*Hilichurl.Add("冰箭丘丘人", "丘丘人");
            Hilichurl.Add("冲锋丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");
            Hilichurl.Add("丘丘人", "丘丘人");*/}

            Directory.Add("史莱姆·水", "史莱姆");
            Directory.Add("史莱姆·火", "史莱姆");
            Directory.Add("史莱姆·冰", "史莱姆");
            Directory.Add("史莱姆·雷", "史莱姆");
            Directory.Add("史莱姆·风", "史莱姆");
            Directory.Add("史莱姆·岩", "史莱姆");
            //Slime.Add("史莱姆·草", "史莱姆");

            Directory.Add("刻晴", "角色");
            Directory.Add("芭芭拉", "角色");
            //Directory.Add("胡桃", "角色");

        }


        public void Init(UnitEntity _unit)
        {
            unitEntity = _unit;
            unit = unitEntity.unit;
        }

        public Unit GetUnit()
        {
            return unit;
        }

        public UnitEntity GetUnitEntity()
        {
            return unitEntity;
        }

        public void Display()
        {
            if(unit==null)
            {
                Debug.LogError("未初始化");
            }

            ReDraw();

            gameObject.SetActive(true);
        }

        public void ReDraw()
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
            ATKText.text = unit.baseATK.ToString();
            //卡牌特性，目前没有，暂定为固定语句
            FeatureText.text = "【战吼】：可爱值提升至上限";
            AttrText.text = TYPE[unit.unitType] + "/" + ELEM[unit.selfElement] + "属性";

            //TODO:BUFF_list
            StringBuilder InfoBuilder = new StringBuilder();
            InfoBuilder.Append("<size=24> 状态：</size>\n");
            InfoBuilder.Append("<size=22> 当前生命值：" + unit.HP + " </size>\n");
            InfoBuilder.Append("<size=22> 当前攻击力：" + unit.baseATK + " </size>\n");
            InfoBuilder.Append("<size=22> 元素挂载：" + ELEM[unitEntity.ElementAttachment.ElementType] + "元素 </size>\n");
            InfoBuilder.Append("<size=22> BUFF：无 </size>");
            InfoText.text = InfoBuilder.ToString();

            string path = picPath + Directory[unit.unitName] + "/" + unit.unitName;

            Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            UnitPic.GetComponent<Image>().sprite = sprite;

        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}