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

        public TextAsset cardData; // 卡牌数据Json

        private void Awake()
        {
            CardLoader.Instance.cardData = Resources.Load(path) as TextAsset;
            LoadCard();
        }

        /// <summary>
        /// 创建卡
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private Card GenerateCard(JObject card)
        {
            switch (card["cardType"].ToString())
            {
                case "charaCard":
                case "monsterCard":
                    return GenerateUnitCard(card);
                case "spellCard":
                    return GenerateSpellCard(card);
                default:
                    throw new System.Exception("未知的卡牌类型");
            }
        }

        private SpellCard GenerateSpellCard(JObject card)
        {
            // 读取基本卡牌信息
            int id = int.Parse(card["cardID"].ToString());
            string cardName = card["cardName_ZH"].ToString();
            string cardType = card["cardType"].ToString();

            JArray infoArray = (JArray)card["cardInfo"];
            string[] cardInfo = infoArray.ToObject<List<string>>().ToArray();
            JObject spellInfo = (JObject)card["unitInfo"];

            int ATK = int.Parse(spellInfo["ATK"].ToString());
            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), spellInfo["ATKElement"].ToString());

            return new SpellCard(id, cardType, cardName, cardInfo, ATK, ATKElement);
        }

        private UnitCard GenerateUnitCard(JObject card)
        {
            // 读取基本卡牌信息
            int id = int.Parse(card["cardID"].ToString());
            string cardName = card["cardName_ZH"].ToString();
            string cardType = card["cardType"].ToString();

            JArray infoArray = (JArray)card["cardInfo"];
            string[] cardInfo = infoArray.ToObject<List<string>>().ToArray();
            JObject unitInfo = (JObject)card["unitInfo"];

            // 设置单位属性
            int HP = int.Parse(unitInfo["HP"].ToString());
            int ATK = int.Parse(unitInfo["ATK"].ToString());

            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), unitInfo["ATKElement"].ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), unitInfo["selfElement"].ToString());


            if (card["cardType"].ToString() == "charaCard")
            {
                // TODO: 获取角色分支
                return new UnitCard(id, cardType, cardName, cardInfo, ATK, HP, ATKElement, selfElement);
            }

            return new UnitCard(id, cardType, cardName, cardInfo, ATK, HP, ATKElement, selfElement);
        }


        /// <summary>
        /// 读取卡牌
        /// </summary>
        public void LoadCard()
        {
            JArray cardArray = JArray.Parse(cardData.text);
            foreach (var item in cardArray)
            {
                JObject data = (JObject)item;
                Card card = GenerateCard(data);
                CardList.Add(card.cardID, card);
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

        public Card GetCardById(int _cardId)
        {
            if (CardList.ContainsKey(_cardId))
            {
                return (Card)CardList[_cardId];
            }
            return null;

        }
    }
}