
namespace Genpai
{
    public sealed class EnumUtil
    {
        private EnumUtil()
        {
        }
        public static UnitType CardTypeToUnitType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.monsterCard: return UnitType.Monster;
                case CardType.bossCard: return UnitType.Boss;
                case CardType.charaCard: return UnitType.Chara;
                default:throw new System.Exception("无法转换");
            }
        }
    }
}
