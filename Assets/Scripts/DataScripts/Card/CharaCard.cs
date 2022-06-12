using System;

namespace Genpai
{

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class CharaCard : UnitCard
    {
        public int MAXMP;
        
        public int BaseSkillId;
        public int EruptSkillId;
        
        public CharaCard()
        {
        }

        public CharaCard(int id, cfg.card.CardType cardType, string cardName, string[] cardInfo, int atk, int hp,
            ElementEnum atkElement, ElementEnum selfElement,int MAXMP, int baseSkill, int eruptSkill) : base(id, cardType, cardName, cardInfo, atk, hp,
            atkElement, selfElement)
        {
            this.MAXMP = MAXMP;
            this.BaseSkillId = baseSkill;
            this.EruptSkillId = eruptSkill;
        }
    }
}
