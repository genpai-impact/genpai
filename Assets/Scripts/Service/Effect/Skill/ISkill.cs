
namespace Genpai
{
    public interface ISkill
    {
        SkillDamageType GetSkillDamageType();
        SkillType GetSkillType();
        void Release(NewUnit soureceUnit, NewUnit targetUnit);
        bool CostAdequate(int cost);
        void Init(int iD, string skillName, SkillType skillType, string skillDesc, int cost);
    }
}
