using System.Collections.Generic;
using cfg.effect;  

namespace Genpai
{
    public class StartSingingSkill : BaseSkill
    {
        public override SkillDamageType GetSkillDamageType()
        {
            return SkillDamageType.Cure;
        }
        public override TargetType GetSelectType()
        {
            return TargetType.Self;
        }

        private const int CureHP = 5;
        public override void Release(Unit sourceUnit, Unit target)
        {
            List<IEffect> CureList = new List<IEffect>();
            CureList.Add(new Cure(sourceUnit, target, CureHP));
            EffectManager.Instance.TakeEffect(new EffectTimeStep(CureList, TimeEffectType.Skill));
        }
    }
}
