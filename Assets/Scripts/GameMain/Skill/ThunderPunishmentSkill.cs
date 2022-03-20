
using System;
using UnityEngine;

namespace Genpai
{
    public class ThunderPunishmentSkill:BaseSkill
    {
        public override SkillDamageType GetSkillDamageType()
        {
            return SkillDamageType.NotNeedTarget;
        }
        public override void Release(UnitEntity sourceUnit, UnitEntity target)
        {
            Debug.Log("run ThunderPunishmentSkill");
        }
    }
}
