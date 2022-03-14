using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public partial class MagicManager
    {

        public bool Preprocessing()
        {
            SpellCard card = spellCard.GetComponent<SpellPlayerController>().spellCard;
            ElementEnum elementEnum = waitingUnitEntity.ATKElement;

            Debug.Log(elementEnum);
            switch (card.GetType().Name)
            {
                case "DamageSpellCard":
                    break;
                case "CureSpellCard":
                    if (card.elementType == elementEnum)
                    {
                        return CurePreprocessing(card as CureSpellCard);
                    }
                    break;
            }
            return true;
        }

        public bool CurePreprocessing(CureSpellCard card)
        {
            SpellElementBuff spellElementBuff= (SpellElementBuff)System.Enum.Parse(typeof(SpellElementBuff), card.spellElementBuff.ToString());
            switch (spellElementBuff)
            {
                case SpellElementBuff.AreaChange:
                    return false;
            }
            return true;
        }

        public void SpellCardEffect()
        {
            SpellCard card = spellCard.GetComponent<SpellPlayerController>().spellCard;
            ElementEnum elementEnum = waitingUnitEntity.ATKElement;

            LinkedList<List<IEffect>> EffectList = new LinkedList<List<IEffect>>();
            switch (card.GetType().Name)
            {
                case "DamageSpellCard":
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
                    if (card.elementType == elementEnum)
                    {
                        Debug.Log("CureAppendix");
                        EffectList.AddLast(CureEffectAppendix(card as CureSpellCard));
                    }
                    else
                    {
                        //Debug.Log("CureNormal");
                        EffectList.AddLast(CureEffectNormal(card as CureSpellCard));
                    }
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

        public List<IEffect> CureEffectAppendix(CureSpellCard card)
        {
            List<IEffect> CureList = new List<IEffect>();
            switch (card.spellElementBuff)
            {
                case SpellElementBuff.AreaChange:
                    for(int i = 0; i < TargetList.Count; i++)
                    {
                        if (TargetList[i])
                        {
                            CureList.Add(new Cure(waitingUnitEntity,
                                BattleFieldManager.Instance.bucketVertexs[i].unitCarry,
                                card.BaseNumerical));
                        }
                    }
                    break;
            }
            return CureList;
        }

        public List<IEffect> CureEffectNormal(CureSpellCard card)
        {
            List<IEffect> CureList = new List<IEffect>();
            Debug.Log("CureEffectNormal");
            CureList.Add(new Cure(waitingUnitEntity, targetUnitEntity, card.BaseNumerical));
            return CureList;
        }
    }
}
