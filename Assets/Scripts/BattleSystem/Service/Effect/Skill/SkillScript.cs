using System.Linq;
using UnityEngine;

namespace Genpai
{
    public class SkillScript : BaseClickHandle
    {
        public void Skill()
        {
            GenpaiMouseDown();
        }

        protected override void DoGenpaiMouseDown()
        {
            UnitEntity unitEntity = GetComponent<UnitEntity>();

            Chara chara = BattleFieldManager.Instance.GetBucketBySerial(unitEntity.carrier.serial).unitCarry as Chara;

            //Chara chara = (unitEntity.unit as Chara);
            //ISkill skill = chara.Erupt;
            //if (!skill.CostAdequate(chara.MP))
            //{
            //    // 测试阶段注释这个即可
            //    return;
            //}
            //MagicManager.Instance.SkillRequest(unitEntity, skill);
            
            int eruptSkillId = LubanLoader.tables.CardItems.DataList.Single(chara => chara.Id == unitEntity.GetUnit().BaseUnit.UnitID).EruptSkill;
            int eruptSkillCost = LubanLoader.tables.SkillItems.DataList.Single(skillItem => skillItem.Id == eruptSkillId).Cost;
            //Debug.Log("判断蓝耗之前");
            //Debug.Log($"eruptSkillId = {eruptSkillId}, eruptSkillCost = {eruptSkillCost}, chara.MP = {chara.MP}");
            if (chara.MP < eruptSkillCost)
            {
                //Debug.Log("蓝不够");
                return;
            }
            SkillManager.Instance.SkillRequest(eruptSkillId, unitEntity.ownerSite);  // 角色主动技能的Request
            chara.MP -= eruptSkillCost;  // 消耗相应的MP
            unitEntity.unitDisplay.FreshUnitUI(new UnitView(unitEntity.GetUnit()));  // 即时刷新MP显示
        }
    }
}
