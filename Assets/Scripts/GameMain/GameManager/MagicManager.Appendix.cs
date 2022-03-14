using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public partial class MagicManager
    {
        public void SpellCardEffect()
        {
            SpellCard card = spellCard.GetComponent<SpellPlayerController>().spellCard;
            ElementEnum elementEnum = waitingUnitEntity.ATKElement;

            LinkedList<List<IEffect>> EffectList = new LinkedList<List<IEffect>>();
            switch (card.GetType().Name)
            {
                case "DamageSpellCard":
                    Debug.Log("DamageSpellCard");
                    if (card.elementType == elementEnum)
                    {
                        EffectList.AddLast(DamageEffectAppendix(card as DamageSpellCard));
                    }
                    else
                    {
                        EffectList.AddLast(DamageEffectNormal(card as DamageSpellCard));
                    }
                    break;
                case "CureSpellCard":
                    break;
                case "BuffSpellCard":
                    break;
            }
            EffectManager.Instance.TakeEffect(EffectList);
        }

        public List<IEffect> DamageEffectAppendix(DamageSpellCard card)
        {
            List<IEffect> DamageList = new List<IEffect>();
            switch (card.spellElementBuff)
            {
                case SpellElementBuff.ElementDamage:
                    ElementEnum elementType = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), card.ElementBuffAppendix.ToString());
                    DamageList.Add(new Damage(waitingUnitEntity, targetUnitEntity,
                        new DamageStruct(card.BaseNumerical, elementType)));
                    break;
            }
            return DamageList;
        }

        public List<IEffect> DamageEffectNormal(DamageSpellCard card)
        {
            List<IEffect> DamageList = new List<IEffect>();
            DamageList.Add(new Damage(waitingUnitEntity, targetUnitEntity,
                        new DamageStruct(card.BaseNumerical, ElementEnum.None)));
            return DamageList;
        }
    }
}
