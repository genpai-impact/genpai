using System.Collections;
using System.Collections.Generic;
using cfg.effect;

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

    public class NewSpellCard : Card
    {
        public ElementEnum BuffElement;

        public EffectConstructProperties baseEffect;

        public EffectConstructProperties buffedEffect;

        public NewSpellCard(int _id, string _cardType, string _cardName, string[] _cardInfo, ElementEnum _buffElement, List<EffectConstructProperties> effectConstructs)
            : base(_id, _cardType, _cardName, _cardInfo)
        {
            BuffElement = _buffElement;
            baseEffect = effectConstructs[0];
            buffedEffect = effectConstructs[1];
        }
    }

}
