
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 通过Card创建的单位数据表单
    /// </summary>
    public class BaseUnit
    {
        public int unitID { get; private set; }
        public string unitName { get; private set; }
        public CardType unitType { get; private set; }

        public int tempHP { get; set; }

        public int baseHP { get; private set; }
        public int baseATK { get; private set; }
        public ElementEnum baseATKElement { get; private set; }
        public ElementEnum baseSelfElement { get; private set; }

        public void Init(UnitCard unitCard)
        {
            unitID = unitCard.cardID;
            unitName = unitCard.cardName;
            unitType = EnumUtil.CardTypeToUnitType(unitCard.cardType);

            tempHP = unitCard.hp;
            baseHP = unitCard.hp;
            baseATK = unitCard.atk;

            baseATKElement = unitCard.atkElement;
            baseSelfElement = unitCard.selfElement;
        }

        public BaseUnit(UnitCard unitCard)
        {
            Init(unitCard);
        }
    }
}