using System;

namespace Genpai
{

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class CharaCard : UnitCard
    {
        public int MAXMP;
        public BaseSkill Warfare;// 元素战技
        public BaseSkill Erupt;// 元素爆发
        public CharaCard()
        {
        }

        public CharaCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, int _hp,
            ElementEnum _atkElement, ElementEnum _selfElement,int MAXMP, BaseSkill Warfare, BaseSkill Erupt) : base(_id, _cardType, _cardName, _cardInfo, _atk, _hp,
            _atkElement, _selfElement)
        {
            this.MAXMP = MAXMP;
            this.Warfare = Warfare;
            this.Erupt = Erupt;
        }
    }
}
