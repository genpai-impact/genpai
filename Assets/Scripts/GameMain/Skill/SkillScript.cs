
using UnityEngine;

namespace Genpai
{
    public class SkillScript : MonoBehaviour
    {
        public void Skill()
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
