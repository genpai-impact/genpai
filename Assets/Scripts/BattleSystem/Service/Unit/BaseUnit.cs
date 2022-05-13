
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 通过Card创建的单位数据表单
    /// </summary>
    public class BaseUnit
    {
        public int UnitID { get; private set; }
        public string UnitName { get; private set; }
        public CardType UnitType { get; private set; }
        public int BaseHp { get; private set; }
        public int BaseAtk { get; private set; }
        public ElementEnum BaseAtkElement { get; private set; }
        public ElementEnum BaseSelfElement { get; private set; }

        private void Init(UnitCard unitCard)
        {
            UnitID = unitCard.cardID;
            UnitName = unitCard.cardName;
            UnitType = EnumUtil.CardTypeToUnitType(unitCard.cardType);

            BaseHp = unitCard.hp;
            BaseAtk = unitCard.atk;

            BaseAtkElement = unitCard.atkElement;
            BaseSelfElement = unitCard.selfElement;
        }

        public BaseUnit(UnitCard unitCard)
        {
            Init(unitCard);
        }
    }
}