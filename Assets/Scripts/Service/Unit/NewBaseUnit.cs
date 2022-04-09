
using System.Collections.Generic;

namespace Genpai
{
    public class NewBaseUnit
    {
        public int unitID { get; private set; }
        public string unitName { get; private set; }
        public UnitType unitType { get; private set; }

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

        public NewBaseUnit(UnitCard unitCard)
        {
            Init(unitCard);
        }
    }
}