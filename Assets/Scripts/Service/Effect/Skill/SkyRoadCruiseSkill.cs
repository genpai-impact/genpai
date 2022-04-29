
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using UnityEngine;

namespace Genpai
{
    //对目标造成2点雷元素伤害，随后对所有敌人随机造成5次1点雷元素伤害
    public class SkyRoadCruiseSkill : BaseSkill
    {
        public override SkillDamageType GetSkillDamageType()
        {
            return SkillDamageType.Attack;
        }

        public override SelectTargetType GetSelectType()
        {
            return SelectTargetType.NotSelf;
        }

        private const int firstDamage = 2;
        private const int randomDamage = 1;
        private const int randomCount = 5;
        private const ElementEnum damageElement = ElementEnum.Electro;

        // fixme 目前技能打怪会被反击，不知道哪里触发的
        public override void Release(Unit sourceUnit, Unit target)
        {
            List<IEffect> DamageList = new List<IEffect>();
            DamageList.Add(new Damage(sourceUnit, target, new DamageStruct(firstDamage, damageElement)));

            //LinkedList<List<IEffect>> EffectList = new LinkedList<List<IEffect>>();
            //EffectList.AddLast(DamageList);
            //EffectManager.Instance.TakeEffect(EffectList);

            LinkedList<EffectTimeStep> EffectList = new LinkedList<EffectTimeStep>();

            EffectList.AddLast(new EffectTimeStep(DamageList, TimeEffectType.Skill));

            for (int i = 0; i < randomCount; i++)
            {
                List<bool> TargetList = BattleFieldManager.Instance.CheckEnemyUnit(sourceUnit.ownerSite);
                List<int> TargetIndex = new List<int>();
                for (int j = 0; j < TargetList.Count; j++)
                {
                    if (TargetList[j])
                    {
                        TargetIndex.Add(j);
                    }
                }
                if (TargetIndex.Count <= 0)
                {
                    break;
                }
                CollectionsUtil.FisherYatesShuffle(TargetIndex);

                List<IEffect> RandomDamageList = new List<IEffect>();
                RandomDamageList.Add(new Damage(sourceUnit, BattleFieldManager.Instance.buckets[TargetIndex[0]].unitCarry,
                    new DamageStruct(randomDamage, damageElement)));

                //LinkedList<List<IEffect>> RandomEffectList = new LinkedList<List<IEffect>>();
                //RandomEffectList.AddLast(RandomDamageList);
                //EffectManager.Instance.TakeEffect(RandomEffectList);

                EffectList.AddLast(new EffectTimeStep(RandomDamageList, TimeEffectType.Appendix));
            }

            EffectManager.Instance.TakeEffect(EffectList);
        }
    }
}
