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
            SpellElementBuff spellElementBuff = (SpellElementBuff)System.Enum.Parse(typeof(SpellElementBuff), card.spellElementBuff.ToString());
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
            //Debug.Log(elementEnum);
            //Debug.Log(card.GetType().Name);
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
                        //Debug.Log("CureAppendix");
                        EffectList.AddLast(CureEffectAppendix(card as CureSpellCard));
                    }
                    else
                    {
                        //Debug.Log("CureNormal");
                        EffectList.AddLast(CureEffectNormal(card as CureSpellCard));
                    }
                    break;
                case "DrawSpellCard":
                    if (card.elementType == elementEnum)
                    {
                        DrawEffectAppendix(card as DrawSpellCard);
                    }
                    else
                    {
                        //Debug.Log("DrawNormal");
                        DrawEffectNormal(card as DrawSpellCard);
                    }
                    break;
                case "BuffSpellCard":
                    if (card.elementType == elementEnum)
                    {
                        EffectList.AddLast(BuffEffectAppendix(card as BuffSpellCard));
                    }
                    else
                    {
                        EffectList.AddLast(BuffEffectNormal(card as BuffSpellCard));
                    }
                    break;
            }
            if (EffectList.Count != 0)
            {
                EffectManager.Instance.TakeEffect(EffectList);
            }
        }

        public List<IEffect> BuffEffectAppendix(BuffSpellCard buffSpellCard)
        {
            List<IEffect> BuffList = new List<IEffect>();
            int appendixNumerical = int.Parse(buffSpellCard.ElementBuffAppendix.ToString());  // 卡牌同元素增强时的数值
            switch (buffSpellCard.buffName)
            {
                case BuffEnum.ATKBuff:
                    //ATKCardBuff atkBuff = ReflectionHelper.CreateInstanceCurrentAssembly<ATKCardBuff>(ATKCardBuff.);
                    List<bool> atkBuffList = BattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);
                    List<GameObject> bucketList = BattleFieldManager.Instance.GetBucketSet(atkBuffList);
                    foreach (GameObject bucket in bucketList)
                    {
                        BuffList.Add(new AddBuff(waitingUnitEntity, bucket.GetComponent<BucketEntity>().unitCarry,
                            new ATKCardBuff(appendixNumerical, 1)));
                    }
                    break;
                case BuffEnum.Shield:
                    BuffList.Add(new AddBuff(waitingUnitEntity, targetUnitEntity,
                        new ShieldBuff(appendixNumerical)));
                    break;
                case BuffEnum.Armor:
                    BuffList.Add(new AddBuff(waitingUnitEntity, targetUnitEntity, 
                        new ArmorBuff(appendixNumerical)));
                    break;
                case BuffEnum.Freeze:
                    BuffList.Add(new AddBuff(waitingUnitEntity, targetUnitEntity,
                        new FreezeBuff(appendixNumerical)));
                    break;
            }
            return BuffList;
        }
        public List<IEffect> BuffEffectNormal(BuffSpellCard buffSpellCard)
        {
            List<IEffect> BuffList = new List<IEffect>();
            int normalNumerical = int.Parse(buffSpellCard.ElementBuffAppendix.ToString());  // 卡牌同元素增强时的数值
            switch (buffSpellCard.buffName)
            {
                case BuffEnum.ATKBuff:
                    //ATKCardBuff atkBuff = ReflectionHelper.CreateInstanceCurrentAssembly<ATKCardBuff>(ATKCardBuff.);
                    List<bool> atkBuffList = BattleFieldManager.Instance.CheckOwnUnit(waitingPlayer);
                    List<GameObject> bucketList = BattleFieldManager.Instance.GetBucketSet(atkBuffList);
                    foreach (GameObject bucket in bucketList)
                    {
                        BuffList.Add(new AddBuff(waitingUnitEntity, bucket.GetComponent<BucketEntity>().unitCarry,
                            new ATKCardBuff(normalNumerical, 1)));
                    }
                    break;
                case BuffEnum.Shield:
                    BuffList.Add(new AddBuff(waitingUnitEntity, targetUnitEntity,
                        new ShieldBuff(normalNumerical)));
                    break;
                case BuffEnum.Armor:
                    BuffList.Add(new AddBuff(waitingUnitEntity, targetUnitEntity,
                        new ArmorBuff(normalNumerical)));
                    break;
                case BuffEnum.Freeze:
                    BuffList.Add(new AddBuff(waitingUnitEntity, targetUnitEntity,
                        new FreezeBuff(normalNumerical)));
                    break;
            }
            return BuffList;
        }
        public void DrawEffectNormal(DrawSpellCard card)
        {
            GenpaiPlayer player = GameContext.Instance.GetPlayerBySite(waitingPlayer);
            player.HandOutCard(1);
            //Debug.Log("Draw one card");
        }
        public void DrawEffectAppendix(DrawSpellCard card)
        {
            GenpaiPlayer player = GameContext.Instance.GetPlayerBySite(waitingPlayer);
            player.HandOutCard(2);
            //Debug.Log("Draw two card");
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
                    for (int i = 0; i < TargetList.Count; i++)
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
