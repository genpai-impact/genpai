using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.card;

namespace Genpai
{
    /// <summary>
    /// 卡牌读取器，在内存中预存所有卡牌
    /// （数据转换由项目根目录DataScripts/JsonConvert.ipynb实现，修改卡牌类记得匹配修改转换脚本）
    /// </summary>
    public class CardLoader : Singleton<CardLoader>
    {
        public Hashtable CardList = new Hashtable();    // 卡牌数据哈希表


        public CardItems cardItems;
        public SpellItems spellItems;

        public void Init()
        {
            cardItems = LubanLoader.tables.CardItems;
            spellItems = LubanLoader.tables.SpellItems;

            // 因为cardloader中使用了skill相关信息，所以必须在这里加载，保证执行顺序。
            // TODO: 删掉SpellCardLoader
            SkillLoader.SkillLoad();
            SpellCardLoader.Instance.LoadSpellCardData();

            LoadCard();
            LoadSpellCard();
        }

        /// <summary>
        /// 读取卡牌
        /// </summary>
        public void LoadCard()
        {

            foreach (var item in cardItems.DataList)
            {
                Card card = GenerateCard(item);
                if (card == null)
                {
                    Debug.Log("LoadCard null " + item);
                    continue;
                }
                CardList.Add(card.cardID, card);
            }
            //foreach (var i in CardList.Keys) Debug.Log("id "+i+" "+(CardList[i] as Card).cardName);
        }

        public void LoadSpellCard()
        {
            foreach (var item in spellItems.DataList)
            {
                Card card = OldGenerateSpellCard(item);
                if (card == null)
                {
                    Debug.Log("LoadCard null " + item);
                    continue;
                }
                CardList.Add(card.cardID, card);
            }
        }

        // TODO: 删掉
        private SpellCard OldGenerateSpellCard(SpellItem card)
        {
            int cardID = card.Id;
            return SpellCardLoader.Instance.GetSpellCard(cardID);
        }

        private NewSpellCard GenerateSpellCard(SpellItem card)
        {
            ElementEnum buffElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.ElementType.ToString());

            return new NewSpellCard(card.Id, card.CardType.ToString(), card.CardName, card.CardInfo.Split('\n'), buffElement, card.EffectInfos);
        }



        /// <summary>
        /// 创建卡
        /// </summary>
        private Card GenerateCard(CardItem card)
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
            int HP = card.HP;
            int ATK = card.ATK;
            int MAXMP = 4;
            int BaseSkillID = card.BaseSkill;
            int EruptSkillID = card.EruptSkill;
            ISkill BaseSkill = SkillLoader.GetSkill(BaseSkillID);
            ISkill EruptSkill = SkillLoader.GetSkill(EruptSkillID);

            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.ATKElement.ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.SelfElement.ToString());

            return new CharaCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, ATK, HP, ATKElement, selfElement,
                MAXMP, BaseSkill, EruptSkill);
        }

        private UnitCard GenerateUnitCard(CardItem card)
        {
            // 读取基本卡牌信息
            CardTemp cardTemp = GetCardBaseInfo(card);

            // 设置单位属性
            int HP = card.HP;
            int ATK = card.ATK;

            ElementEnum ATKElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.ATKElement.ToString());
            ElementEnum selfElement = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.SelfElement.ToString());

            return new UnitCard(cardTemp.id, cardTemp.cardType, cardTemp.cardName, cardTemp.cardInfo, ATK, HP, ATKElement, selfElement);
        }

        private CardTemp GetCardBaseInfo(CardItem card)
        {
            int id = card.Id;
            string cardName = card.CardNameZh;
            string cardType = card.CardType.ToString();

            string[] cardInfo = card.CardInfo.Split('\n');

            return new CardTemp(id, cardName, cardType, cardInfo);
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
           // foreach (var i in ret) Debug.Log(i.cardName+" "+i.cardInfo);
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
            public string[] cardInfo;

            public CardTemp(int id, string cardName, string cardType,
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