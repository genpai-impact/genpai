
namespace Genpai
{
    public interface ISkill
    {
        SelectTargetType GetSelectType();
        SkillDamageType GetSkillDamageType();
        SkillType GetSkillType();
        void Release(Unit soureceUnit, Unit targetUnit);
        bool CostAdequate(int cost);
        void Init(int iD, string skillName, SkillType skillType, string skillDesc, int cost);
    }
}
