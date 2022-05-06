using System;

namespace Genpai
{

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class CharaCard : UnitCard
    {
        public int MAXMP;
        public ISkill Warfare;// 即出场技能
        public ISkill Erupt;// 即Skill，主动技能
        public CharaCard()
        {
        }

        public CharaCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, int _hp,
            ElementEnum _atkElement, ElementEnum _selfElement,int MAXMP, ISkill Warfare, ISkill Erupt) : base(_id, _cardType, _cardName, _cardInfo, _atk, _hp,
            _atkElement, _selfElement)
        {
            this.MAXMP = MAXMP;
            this.Warfare = Warfare;
            this.Erupt = Erupt;
        }
    }
}
