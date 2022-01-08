using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    public class CardLoader : MonoBehaviour
    {
        public Hashtable CardList = new Hashtable();
        public List<int> CardIDList = new List<int>(); // 测试用

        public TextAsset cardData; // 卡牌数据Json

        private void Awake()
        {
            LoadCard();
        }

        public void LoadCard()
        {
            JArray cardArray = JArray.Parse(cardData.text);
            foreach (var item in cardArray)
            {

                JObject card = (JObject)item;

                // 读取基本卡牌信息
                int id = int.Parse(card["cardID"].ToString());
                string cardName = card["cardName_ZH"].ToString();
                string cardType = card["cardType"].ToString();

                JArray infoArray = (JArray)card["cardInfo"];
                string[] cardInfo = infoArray.ToObject<List<string>>().ToArray();

                CardIDList.Add(id); // 测试用

                // 读取特殊卡牌信息
                if (card["cardType"].ToString() == "charaCard" || card["cardType"].ToString() == "monsterCard")
                {
                    JObject unitInfo = (JObject)card["unitInfo"];

                    // 设置单位属性
                    int HP = int.Parse(unitInfo["HP"].ToString());
                    int ATK = int.Parse(unitInfo["ATK"].ToString());

                    ElementEnum ATKElement = ElementEnum.None;
                    ElementEnum selfElement = ElementEnum.None;
                    try
                    {
                        ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), unitInfo["ATKElement"].ToString());
                        selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), unitInfo["selfElement"].ToString());
                    }
                    catch
                    {
                        Debug.Log(cardName + "卡牌数据元素设置有误");
                    }

                    if (card["cardType"].ToString() == "monsterCard")
                    {
                        // 角色卡怪物卡区别未设置
                        CardList.Add(id, new UnitCard(id, cardType, cardName, cardInfo, ATK, HP, ATKElement, selfElement));
                    }
                    else
                    {
                        // 角色充能分支未设置
                        CardList.Add(id, new UnitCard(id, cardType, cardName, cardInfo, ATK, HP, ATKElement, selfElement));

                    }

                    Debug.Log(cardName + " 已收录");
                }
            }
        }
    }
}