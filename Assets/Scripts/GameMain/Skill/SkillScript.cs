
namespace Genpai
{
    public class SkillScript : BaseClickHandle
    {
        public void Skill()
        {
            GenpaiMouseDown();
        }
        public override void DoGenpaiMouseDown()
        {
            UnitEntity unitEntity = GetComponent<UnitEntity>();
            Chara chara = (unitEntity.unit as Chara);
            ISkill skill = chara.Erupt;
            if (!skill.CostAdequate(chara.MP))
            {
                // 测试阶段注释这个即可
                return;
            }
            MagicManager.Instance.SkillRequest(unitEntity, skill);
            chara.MP = 0;
        }
    }
}
