﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Genpai
{
    /// <summary>
    /// 旧卡牌读取器
    /// TODO: 在魔法卡重构后销毁
    /// </summary>
    public class CardLoader : MonoSingleton<CardLoader>
    {
        private string path = "Data\\CardData";
        public Hashtable CardList = new Hashtable();    // 卡牌数据哈希表

        public TextAsset cardData; // 卡牌数据Json

        private void Awake()
        {
            // 因为cardloader中使用了skill相关信息，所以必须在这里加载，保证执行顺序。
            SkillLoader.SkillLoad();
            SpellCardLoader.Instance.LoadSpellCardData();
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
                case "Chara":
                    return GenerateCharaCard(card);
                case "Boss":
                    return GenerateBossCard(card);
                case "Monster":
                    return GenerateUnitCard(card);
                case "Spell":
                    return GenerateSpellCard(card);
                default:
                    throw new System.Exception("未知的卡牌类型");
            }
        }

        private SpellCard GenerateSpellCard(JObject card)
        {
            int cardID = int.Parse(card["cardID"].ToString());
            return SpellCardLoader.Instance.GetSpellCard(cardID);
        }

        private UnitCard GenerateBossCard(JObject card)
        {
            return GenerateUnitCard(card);
        }

        private UnitCard GenerateCharaCard(JObject card)
        {
            // 读取基本卡牌信息
            CardTemp cardTemp = GetCardBaseInfo(card);

            // 设置单位属性
            int HP = int.Parse(cardTemp.unitInfo["HP"].ToString());
            int ATK = int.Parse(cardTemp.unitInfo["ATK"].ToString());
            int MAXMP = int.Parse(cardTemp.charaInfo["MAXMP"].ToString());
            int WarfareID = int.Parse(cardTemp.charaInfo["WarfareSkillID"].ToString());
            int EruptID = int.Parse(cardTemp.charaInfo["EruptSkillID"].ToString());
            ISkill WarfareSkill = SkillLoader.GetSkill(WarfareID);
            ISkill EruptSkill = SkillLoader.GetSkill(EruptID);

            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["ATKElement"].ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), cardTemp.unitInfo["selfElement"].ToString());

            return new CharaCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, ATK, HP, ATKElement, selfElement,
                MAXMP, WarfareSkill, EruptSkill);
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
            JObject charaInfo = (JObject)card["charaInfo"];
            return new CardTemp(id, cardName, cardType, infoArray, cardInfo, unitInfo, charaInfo);
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
            public JObject charaInfo;

            public CardTemp(int id, string cardName, string cardType,
                JArray infoArray, string[] cardInfo, JObject unitInfo, JObject charaInfo)
            {
                this.id = id;
                this.cardName = cardName;
                this.cardType = cardType;
                this.infoArray = infoArray;
                this.cardInfo = cardInfo;
                this.unitInfo = unitInfo;
                this.charaInfo = charaInfo;
            }
        }
    }
}