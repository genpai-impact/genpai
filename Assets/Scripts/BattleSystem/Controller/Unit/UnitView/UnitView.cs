using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Genpai
{
    /// <summary>
    /// 单位快速视图
    /// </summary>
    public class UnitView
    {
        public int UnitID;
        public readonly string UnitName;
        public readonly CardType UnitType;

        // >>> 单位面板
        public readonly int Hp;
        public readonly int Atk;
        public readonly ElementEnum AtkElement;
        public readonly ElementEnum SelfElement;

        public int Mp;

        public readonly int EruptMp;

        // >>> Info信息
        public readonly List<BuffView> BuffViews;
        // public List<SkillInfo> skillInfos;

        public UnitView(Unit unit)
        {
            UnitID = unit.BaseUnit.UnitID;
            UnitName = unit.UnitName;
            UnitType = unit.UnitType;

            Hp = unit.Hp;
            Atk = unit.Atk;
            
            AtkElement = unit.AtkElement;
            SelfElement = unit.SelfElement.ElementType;
        
            if(unit.GetType().Name=="Chara"){
                EruptMp = ((BaseSkill)((Chara)unit).Erupt).Cost;
            }

            // 更新Buff信息
            BuffViews = new List<BuffView>();
            
            // 查找已激活Buff
            List<Buff> buffs = BuffManager.Instance.GetBuffByUnit(unit).Select(pair => pair.Buff).ToList();
            foreach (var buff in buffs)
            {
                // Debug.Log(UnitName+"中了"+buff.BuffName);
                BuffViews.Add(new BuffView(buff));
            }
        }
    }
}