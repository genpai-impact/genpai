
namespace Genpai
{
    public sealed class EnumUtil
    {
        private EnumUtil()
        {
        }
        public static UnitType CardTypeToUnitType(cfg.card.CardType cardType)
        {
            switch (cardType)
            {
                case cfg.card.CardType.Monster: return UnitType.Monster;
                case cfg.card.CardType.Boss: return UnitType.Boss;
                case cfg.card.CardType.Chara: return UnitType.Chara;
                default: throw new System.Exception("无法转换");
            }
        }
    }
}
