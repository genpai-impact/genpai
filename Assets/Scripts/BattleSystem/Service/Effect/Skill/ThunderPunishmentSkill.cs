
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class ThunderPunishmentSkill : BaseSkill
    {
        public override SkillDamageType GetSkillDamageType()
        {
            return SkillDamageType.NotNeedTarget;
        }

        public override SelectTargetType GetSelectType()
        {
            return SelectTargetType.None;
        }

        private const int RoundCount = 1;

        public override void Release(Unit sourceUnit, Unit target)
        {
            List<bool> TargetList = BattleFieldManager.Instance.CheckOwnUnit(sourceUnit.OwnerSite);

            List<IEffect> BuffList = new List<IEffect>();

            for (int i = 0; i < TargetList.Count; i++)
            {
                if (TargetList[i])
                {

                    AttackElementBuff buff = new AttackElementBuff(BuffEnum.ElectroAttack, ElementEnum.Electro, RoundCount);
                    BuffList.Add(new AddBuff(sourceUnit, BattleFieldManager.Instance.Buckets[i].unitCarry, buff));
                }
            }

            EffectManager.Instance.TakeEffect(new EffectTimeStep(BuffList, TimeEffectType.Skill));

        }
    }
}




