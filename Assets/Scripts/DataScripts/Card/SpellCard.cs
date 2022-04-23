namespace Genpai
{
    public class SpellCard : Card
    {
        public ISpell Spell;

        public SpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, ISpell _spell)
            : base(_id, _cardType, _cardName, _cardInfo)
        {
            Spell = _spell;
        }
    }
}
