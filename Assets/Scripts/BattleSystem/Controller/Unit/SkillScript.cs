﻿using System.Linq;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Unit;
using DataScripts;
using UnityEngine;

namespace BattleSystem.Controller.Unit
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
            var unitEntity = GetComponent<UnitEntity>();

            if (!(BattleFieldManager.Instance.GetBucketBySerial(unitEntity.carrier.serial).unitCarry is Chara chara)) { return; }
            
            var eruptSkillCost = LubanLoader.GetTables().SkillItems.DataList.Single(skillItem => skillItem.Id == chara.EruptSkillId).Cost;
            if (chara.MP < eruptSkillCost)
            {
                Debug.Log("蓝不够");
                return;
            }
            SkillManager.Instance.SkillRequest(chara.EruptSkillId, unitEntity);  // 角色主动技能的Request
        }
    }
}
