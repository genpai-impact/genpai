
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

        private const int RoundCount = 0;

        public override void Release(Unit sourceUnit, Unit target)
        {
            List<bool> TargetList = BattleFieldManager.Instance.CheckOwnUnit(sourceUnit.ownerSite);
            for (int i = 0; i < TargetList.Count; i++)
            {
                if (TargetList[i])
                {
                    Unit unit = BattleFieldManager.Instance.buckets[i].unitCarry;
                    AttackElementBuff buff = new AttackElementBuff(BuffEnum.ElectroAttack, ElementEnum.Electro, RoundCount);
                    buff.AddBuff(unit);
                }
            }
        }
    }
}
