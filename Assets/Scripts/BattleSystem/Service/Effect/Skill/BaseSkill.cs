using System;
using UnityEngine;
using cfg.effect;  
namespace Genpai
{
    public enum SkillDamageType
    {
        Attack,
        Cure,
        NotNeedTarget,
    }
    public abstract class BaseSkill : ISkill
    {
        public int ID;
        public string SkillName;
        public SkillType SkillType;
        public string SkillDesc;
        public int Cost;

        public void Init(int iD, string skillName, SkillType skillType, string skillDesc, int cost)
        {
            ID = iD;
            SkillName = skillName;
            SkillType = skillType;
            SkillDesc = skillDesc;
            Cost = cost;
        }

        public bool CostAdequate(int cost)
        {
            return cost >= Cost;
        }

        public abstract SkillDamageType GetSkillDamageType();

        public SkillType GetSkillType()
        {
            return SkillType;
        }

        public abstract void Release(Unit sourceUnit, Unit targetUnit);

        public abstract TargetType GetSelectType();

    }
}
