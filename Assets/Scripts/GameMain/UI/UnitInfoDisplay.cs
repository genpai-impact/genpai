using System.Collections;
using System.Collections.Generic;
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

        private UnitEntity unitEntity;
        private Unit unit;

        public void Start()
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
            Hide();
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


            /*Debug.LogError(unit.unitType);
            Debug.LogError(TYPE.Count);*/
            /*AttrText.text = TYPE[unit.unitType] +
            "/" + ELEM[unit.selfElement];*/

            //TODO:BUFF_list
            /*InfoText.text =
                "< size = 24 > 状态：</ size >\n" +
                "< size = 22 > 当前生命值：" + unit.HP + " </ size >\n" +
                "< size = 22 > 当前攻击力：" + unit.baseATK + " </ size > \n" +
                "< size = 22 > 元素挂载：" + "无"+ " </ size >\n" +
                                        //ELEM[unitEntity.ElementAttachment.ElementType] + " </ size >\n" +
                "< size = 22 > BUFF：无 </ size >";
            InfoText.text = "xxx";*/

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}