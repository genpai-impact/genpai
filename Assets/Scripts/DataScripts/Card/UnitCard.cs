using System;

namespace Genpai
{

    /// <summary>
    /// 单位卡，加入单位特征的卡牌
    /// </summary>
    public class UnitCard : Card
    {
        public int atk;
        public int hp;
        public ElementEnum atkElement;
        public ElementEnum selfElement;

        public UnitCard()
        {
        }

        public UnitCard(int _id, string _cardType, string _cardName, string[] _cardInfo, int _atk, int _hp,
            ElementEnum _atkElement, ElementEnum _selfElement) : base(_id, _cardType, _cardName, _cardInfo)
        {
            this.atk = _atk;
            this.hp = _hp;
            this.atkElement = _atkElement;
            this.selfElement = _selfElement;
        }
    }
}
