using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Excel;

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
            SpellCardLoader.Instance.SpellCardDataList = SpellCardLoader.Instance.SpellCardLoad();
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
                    return GenerateCharaCard(card);
                case "monsterCard":
                    return GenerateUnitCard(card);
                case "spellCard":
                    return GenerateSpellCard(card);
                default:
                    throw new System.Exception("未知的卡牌类型");
            }
        }

        /// <summary>
        /// TODO: 魔法卡应有可选择目标数量，现在暂时为0，即不需要选择
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private SpellCard GenerateSpellCard(JObject card)
        {
            int cardIndex = int.Parse(card["cardIndex"].ToString());
            int cardID = int.Parse(card["cardID"].ToString());
            //Debug.Log(cardID);
            SpellCard spell=SpellCardLoader.Instance.GetSpellCard(cardIndex,cardID);
            return spell;

            //string spellType = cardTemp.unitInfo["spellType"].ToString();
            //
            ////Debug.Log(spellType);
            //switch (spellType)
            //{
            //    case "Damage":
            //        {
            //            int ATK = int.Parse(cardTemp.unitInfo["ATK"].ToString());
            //            ElementEnum Element = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["Element"].ToString());
            //            return new DamageSpellCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo,
            //                SpellType.Damage, Element, ATK);
            //        }
            //    case "Cure":
            //        {
            //            int HP = int.Parse(cardTemp.unitInfo["HP"].ToString());
            //            ElementEnum Element = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["Element"].ToString());
            //            //Debug.Log("Cure Loaded");
            //            return new CureSpellCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo,
            //                SpellType.Cure, Element, HP);
            //        }
            //    case "Buff":
            //        {
            //            int buffNum = int.Parse(cardTemp.unitInfo["BuffNum"].ToString());
            //            ElementEnum Element = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["Element"].ToString());
            //            BuffEnum BuffName = (BuffEnum)System.Enum.Parse(typeof(BuffEnum), cardTemp.unitInfo["BuffName"].ToString());
            //            Debug.Log("BuffCard Loaded");
            //            return new BuffSpellCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo,
            //                SpellType.Buff, Element, buffNum, BuffName);
            //        }
            //}
            //Debug.Log("卡牌加载异常:" + spellType);
            //return null;
        }

        private UnitCard GenerateCharaCard(JObject card)
        {
            // 读取基本卡牌信息
            CardTemp cardTemp = GetCardBaseInfo(card);

            // 设置单位属性
            int HP = int.Parse(cardTemp.unitInfo["HP"].ToString());
            int ATK = int.Parse(cardTemp.unitInfo["ATK"].ToString());

            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["ATKElement"].ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["selfElement"].ToString());

            return new UnitCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, ATK, HP, ATKElement, selfElement);
        }

        private UnitCard GenerateUnitCard(JObject card)
        {
            // 读取基本卡牌信息
            CardTemp cardTemp = GetCardBaseInfo(card);

            // 设置单位属性
            int HP = int.Parse(cardTemp.unitInfo["HP"].ToString());
            int ATK = int.Parse(cardTemp.unitInfo["ATK"].ToString());

            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["ATKElement"].ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["selfElement"].ToString());

            return new UnitCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, ATK, HP, ATKElement, selfElement);
        }


        private CardTemp GetCardBaseInfo(JObject card)
        {
            int id = int.Parse(card["cardID"].ToString());
            string cardName = card["cardName_ZH"].ToString();
            string cardType = card["cardType"].ToString();

            JArray infoArray = (JArray)card["cardInfo"];
            string[] cardInfo = infoArray.ToObject<List<string>>().ToArray();
            JObject unitInfo = (JObject)card["unitInfo"];
            return new CardTemp(id, cardName, cardType, infoArray, cardInfo, unitInfo);
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
                if (card == null)
                {
                    Debug.Log("LoadCard null " + item);
                    continue;
                }
                CardList.Add(card.cardID, card);
            }
            Debug.Log("LoadCard");
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

        private class CardTemp
        {

            public int id;
            public string cardName;
            public string cardType;
            public JArray infoArray;
            public string[] cardInfo;
            public JObject unitInfo;

            public CardTemp(int id, string cardName, string cardType, JArray infoArray, string[] cardInfo, JObject unitInfo)
            {
                this.id = id;
                this.cardName = cardName;
                this.cardType = cardType;
                this.infoArray = infoArray;
                this.cardInfo = cardInfo;
                this.unitInfo = unitInfo;
            }
        }
    }
}