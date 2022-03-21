
namespace Genpai
{
    public interface ISkill
    {
        SkillDamageType GetSkillDamageType();
        SkillType GetSkillType();
        void Release(UnitEntity soureceUnit, UnitEntity targetUnit);
        bool CostAdequate(int cost);
        void Init(int iD, string skillName, SkillType skillType, string skillDesc, int cost);
    }
}
