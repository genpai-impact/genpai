using System.Collections;
using System.Collections.Generic;
using BattleSystem.Service.Element;
using cfg.card;
using DataScripts.Card;
using UnityEngine;
using Utils;

namespace DataScripts.DataLoader
{
    /// <summary>
    /// 卡牌读取器，在内存中预存所有卡牌
    /// （数据转换由项目根目录DataScripts/JsonConvert.ipynb实现，修改卡牌类记得匹配修改转换脚本）
    /// </summary>
    public class CardLoader : Singleton<CardLoader>
    {
        public Hashtable CardList = new Hashtable();    // 卡牌数据哈希表


        public CardItems CardItems;
        public SpellItems SpellItems;

        public void Init()
        {
            CardItems = LubanLoader.GetTables().CardItems;
            SpellItems = LubanLoader.GetTables().SpellItems;
            
            SkillLoader.SkillLoad();

            LoadCard();
            LoadSpellCard();
        }

        /// <summary>
        /// 读取卡牌
        /// </summary>
        public void LoadCard()
        {
            foreach (var item in CardItems.DataList)
            {
                Card.Card card = GenerateCard(item);
                if (card == null)
                {
                    Debug.Log("LoadCard null " + item);
                    continue;
                }
                if(!CardList.ContainsKey(card.CardID)) CardList.Add(card.CardID, card);
            }
            //foreach (var i in CardList.Keys) Debug.Log("id "+i+" "+(CardList[i] as Card).cardName);
        }

        public void LoadSpellCard()
        {
            foreach (var item in SpellItems.DataList)
            {
                    //Card card = OldGenerateSpellCard(item);
                    Card.Card card = GenerateSpellCard(item);
                if (card == null)
                {
                    Debug.Log("LoadCard null " + item);
                    continue;
                }
                if(!CardList.ContainsKey(card.CardID))  CardList.Add(card.CardID, card);    
            }
        }
        
        private SpellCard GenerateSpellCard(SpellItem card)
        {
            ElementEnum buffElement = EnumUtil.ToEnum<ElementEnum>(card.ElementType.ToString());

            return new SpellCard(card.Id, card.CardType, card.CardName, card.CardInfo.Split('\n'), buffElement, card.EffectInfos);
        }
        
        /// <summary>
        /// 创建卡
        /// </summary>
        private Card.Card GenerateCard(CardItem card)
        {
            switch (card.CardType)
            {
                case cfg.card.CardType.Chara:
                    return GenerateCharaCard(card);
                case cfg.card.CardType.Boss:
                    return GenerateBossCard(card);
                case cfg.card.CardType.Monster:
                    return GenerateUnitCard(card);
                default:
                    throw new System.Exception("未知的卡牌类型");
            }
        }



        private UnitCard GenerateBossCard(CardItem card)
        {
            return GenerateUnitCard(card);
        }

        private UnitCard GenerateCharaCard(CardItem card)
        {
            // 读取基本卡牌信息
            CardTemp cardTemp = GetCardBaseInfo(card);

            // 设置单位属性
            int hp = card.HP;
            int atk = card.ATK;
            const int maxMp = 4;
            int baseSkillID = card.BaseSkill;
            int eruptSkillID = card.EruptSkill;

            ElementEnum atkElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.ATKElement.ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.SelfElement.ToString());

            return new CharaCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, atk, hp, atkElement, selfElement,
                maxMp, baseSkillID, eruptSkillID);
        }

        private UnitCard GenerateUnitCard(CardItem card)
        {
            // 读取基本卡牌信息
            CardTemp cardTemp = GetCardBaseInfo(card);

            // 设置单位属性
            int hp = card.HP;
            int atk = card.ATK;

            ElementEnum atkElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.ATKElement.ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.SelfElement.ToString());

            return new UnitCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, atk, hp, atkElement, selfElement);
        }

        private CardTemp GetCardBaseInfo(CardItem card)
        {
            int id = card.Id;
            string cardName = card.CardNameZh;
            cfg.card.CardType cardType = card.CardType;

            string[] cardInfo = card.CardInfo.Split('\n');

            return new CardTemp(id, cardName, cardType, cardInfo);
        }



        /// <summary>
        /// 从卡牌缓存中根据卡牌id列表返回卡组
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public List<Card.Card> GetCardByIds(List<int> cardId)
        {
            List<Card.Card> ret = new List<Card.Card>();
            foreach (int id in cardId)
            {
                if (CardList.ContainsKey(id))
                {
                    ret.Add((Card.Card)CardList[id]);
                }
                else
                {
                    Debug.Log("编号：" + id + " 不存在");
                }
            }
           // foreach (var i in ret) Debug.Log(i.cardName+" "+i.cardInfo);
            return ret;
        }

        public Card.Card GetCardById(int cardId)
        {
            if (CardList.ContainsKey(cardId))
            {
                return (Card.Card)CardList[cardId];
            }
            return null;
        }

        private class CardTemp
        {
            public int id;
            public string cardName;
            public cfg.card.CardType cardType;
            public string[] cardInfo;

            public CardTemp(int id, string cardName, cfg.card.CardType cardType,
                 string[] cardInfo)
            {
                this.id = id;
                this.cardName = cardName;
                this.cardType = cardType;
                this.cardInfo = cardInfo;
            }
        }
    }
}