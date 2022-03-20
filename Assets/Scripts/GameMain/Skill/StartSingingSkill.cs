using System.Collections.Generic;

namespace Genpai
{
    public class StartSingingSkill:BaseSkill
    {
        public override SkillDamageType GetSkillDamageType()
        {
            return SkillDamageType.Cure;
        }
        private const int CureHP = 5;
        public override void Release(UnitEntity sourceUnit, UnitEntity target)
        {
            List<IEffect> CureList = new List<IEffect>();
            CureList.Add(new Cure(sourceUnit, target, CureHP));
            LinkedList<List<IEffect>> EffectList = new LinkedList<List<IEffect>>();
            EffectList.AddLast(CureList);
            EffectManager.Instance.TakeEffect(EffectList);
        }
    }
}
