using System.Collections.Generic;
using System.Linq;
using BattleSystem.Service.Buff;
using BattleSystem.Service.Element;
using BattleSystem.Service.Unit;
using DataScripts;

namespace BattleSystem.Controller.Unit.UnitView
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

        // 似乎Boss分了两个Mp，但是其Mp值记录了Mp_2，不知道是什么原理。
        // 这里加了两个Mp，用于在UnitView中区分，如果不对的话和我说下，我得改改:)
        public int Mp_1;
        public int Mp_2;

        public int Mp;

        public readonly int EruptMp;

        // >>> Info信息
        public readonly List<BuffView> BuffViews;
        // public List<SkillInfo> skillInfos;

        public UnitView(Service.Unit.Unit unit)
        {
            UnitID = unit.BaseUnit.UnitID;
            UnitName = unit.UnitName;
            UnitType = unit.UnitType;

            Hp = unit.Hp;
            Atk = unit.Atk;
            
            AtkElement = unit.AtkElement;
            SelfElement = unit.SelfElement.ElementType;
        
            if(unit is Chara chara){
                EruptMp = LubanLoader.GetTables().SkillItems.DataList.Single(skillItem => skillItem.Id == chara.EruptSkillId).Cost;
            }
            
            // 对于Boss类，分别记录两个技能的Mp
            if(unit is Boss boss){
                Mp_1 = boss.MP_1;
                Mp_2 = boss.MP_2;
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