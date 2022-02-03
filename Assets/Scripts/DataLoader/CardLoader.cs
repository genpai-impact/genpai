using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    /// <summary>
    /// 卡牌读取器，在内存中预存所有卡牌
    /// （数据转换由项目根目录DataScripts/JsonConvert.ipynb实现，修改卡牌类记得匹配修改转换脚本）
    /// </summary>
    public class CardLoader : MonoSingleton<CardLoader>
    {
        private string path = "Data\\CardData";
        public Hashtable CardList = new Hashtable();    // 卡牌数据哈希表
        public List<int> CardIDList = new List<int>();  // 测试随机抽卡用（id不连续

        public TextAsset cardData; // 卡牌数据Json

        private void Awake()
        {
            CardLoader.Instance.cardData = Resources.Load(path) as TextAsset;
            LoadCard();
        }

        /// <summary>
        /// 读取卡牌
        /// </summary>
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

                    //Debug.Log(cardName + " 已收录");
                }
                else
                {
                    JObject spellInfo = (JObject)card["unitInfo"];

                    // 设置单位属性

                    int ATK = int.Parse(spellInfo["ATK"].ToString());

                    ElementEnum ATKElement = ElementEnum.None;

                    try
                    {
                        ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), spellInfo["ATKElement"].ToString());
                    }
                    catch
                    {
                        Debug.Log(cardName + "卡牌数据元素设置有误");
                    }

                    CardList.Add(id, new SpellCard(id, cardType, cardName, cardInfo, ATK, ATKElement));
                    // Debug.Log(cardName + " 已收录");

                }
            }


        }

        /// <summary>
        /// 从卡牌缓存中根据卡牌id列表返回卡组
        /// </summary>
        /// <param name="_cardId"></param>
        /// <returns></returns>
        public List<Card> GetCardByIds(List<int> _cardId)
        {

            List<Card> ret = new List<Card>();

            foreach (int id in _cardId)
            {
                if (CardList.ContainsKey(id))
                {
                    ret.Add((Card)CardList[id]);
                }
                else
                {
                    Debug.Log("编号：" + id + " 不存在");
                }
            }

            return ret;
        }

        public Card GetCardByIds(int _cardId)
        {
            if (CardList.ContainsKey(_cardId))
            {
                return (Card)CardList[_cardId];
            }
            return null;

        }
    }
}