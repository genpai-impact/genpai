using System.Linq;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于通过点击MP框触发角色主动技能的Request
    /// </summary>
    public class SkillScript : BaseClickHandle
    {
        public void Skill()
        {
            GenpaiMouseDown();
        }

        protected override void DoGenpaiMouseDown()  // 由其基类方法GenpaiMouseDown调用
        {
            UnitEntity unitEntity = GetComponent<UnitEntity>();

            Chara chara = BattleFieldManager.Instance.GetBucketBySerial(unitEntity.carrier.serial).unitCarry as Chara;            
            int eruptSkillId = LubanLoader.tables.CardItems.DataList.Single(chara => chara.Id == unitEntity.GetUnit().BaseUnit.UnitID).EruptSkill;
            int eruptSkillCost = LubanLoader.tables.SkillItems.DataList.Single(skillItem => skillItem.Id == eruptSkillId).Cost;
            if (chara.MP < eruptSkillCost)
            {
                Debug.Log("蓝不够");
                return;
            }
            SkillManager.Instance.SkillRequest(eruptSkillId, unitEntity);  // 角色主动技能的Request
        }
    }
}
