
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 全体队友[恢复]4点HP
    /// </summary>
    public class ShiningMiracleSkill : BaseSkill
    {
        public override SkillDamageType GetSkillDamageType()
        {
            return SkillDamageType.NotNeedTarget;
        }

        private const int CureHP = 4;

        public override void Release(Unit sourceUnit, Unit target)
        {
            List<bool> TargetList = NewBattleFieldManager.Instance.CheckOwnUnit(sourceUnit.ownerSite);
            List<IEffect> CureList = new List<IEffect>();
            for (int i = 0; i < TargetList.Count; i++)
            {
                if (TargetList[i])
                {
                    CureList.Add(new Cure(sourceUnit,
                        NewBattleFieldManager.Instance.buckets[i].unitCarry,
                        CureHP));
                }
            }
            LinkedList<List<IEffect>> EffectList = new LinkedList<List<IEffect>>();
            EffectList.AddLast(CureList);
            if (EffectList.Count > 0)
            {
                EffectManager.Instance.TakeEffect(EffectList);
            }
        }
    }
}
