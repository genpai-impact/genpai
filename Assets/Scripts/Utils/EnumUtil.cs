
namespace Genpai
{
    public sealed class EnumUtil
    {
        private EnumUtil()
        {
        }
        public static CardType CardTypeToUnitType(cfg.card.CardType cardType)
        {
            switch (cardType)
            {
                case cfg.card.CardType.Monster: return CardType.Monster;
                case cfg.card.CardType.Boss: return CardType.Boss;
                case cfg.card.CardType.Chara: return CardType.Chara;
                default: throw new System.Exception("无法转换");
            }
        }
    }
}
