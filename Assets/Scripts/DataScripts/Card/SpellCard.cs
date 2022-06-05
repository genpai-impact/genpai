using System.Collections;
using System.Collections.Generic;
using cfg.effect;

namespace Genpai
{
    public class SpellCard : Card
    {
        public ElementEnum BuffElement;
        public EffectConstructProperties baseEffect;
        public EffectConstructProperties buffedEffect;

        public SpellCard(
            int id, cfg.card.CardType cardType, string cardName, string[] cardInfo, 
            ElementEnum buffElement, List<EffectConstructProperties> effectConstructs
            ): base(id, cardType, cardName, cardInfo)
        {
            BuffElement = buffElement;
            baseEffect = effectConstructs[0];
            buffedEffect = effectConstructs[1];
        }
    }

}
