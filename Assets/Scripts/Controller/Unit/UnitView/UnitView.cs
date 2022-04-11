using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 单位快速视图
    /// </summary>
    public class UnitView
    {
        public int unitID;
        public string unitName;
        public UnitType unitType;

        // >>> 单位面板
        public int HP;
        public int ATK;
        public ElementEnum ATKElement;
        public ElementEnum SelfElement;

        public int MP;

        // >>> Info信息
        public List<BuffView> buffViews;
        // public List<SkillInfo> skillInfos;

        public UnitView(Unit unit)
        {
            unitID = unit.unit.unitID;
            unitName = unit.unitName;
            unitType = unit.unitType;

            HP = unit.HP;
            ATK = unit.ATK;

            ATKElement = unit.ATKElement;
            SelfElement = unit.SelfElement.ElementType;

            // 更新Buff信息
            buffViews = new List<BuffView>();

            // 查找已激活Buff
            foreach (var buff in unit.buffAttachment.FindAll(buff => buff.trigger))
            {
                // TODO：Buff重构后更新
                // Debug.Log("Add Buff View");
                buffViews.Add(new BuffView(buff));

            }
        }
    }
}