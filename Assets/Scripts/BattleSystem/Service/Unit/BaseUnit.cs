using BattleSystem.Service.Element;
using DataScripts.Card;
using Utils;

namespace BattleSystem.Service.Unit
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
            UnitID = unitCard.CardID;
            UnitName = unitCard.CardName;
            UnitType = EnumUtil.CardTypeToUnitType(unitCard.CardType);

            BaseHp = unitCard.Hp;
            BaseAtk = unitCard.Atk;

            BaseAtkElement = unitCard.AtkElement;
            BaseSelfElement = unitCard.SelfElement;
        }

        public BaseUnit(UnitCard unitCard)
        {
            Init(unitCard);
        }
    }
}