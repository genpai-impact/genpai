using System;
using UnityEngine;

namespace Genpai
{
    public enum SkillDamageType
    {
        Attack,
        Cure,
    }
    public abstract class BaseSkill
    {
        public int ID;
        public string SkillName;
        public SkillType SkillType;
        public string SkillDesc;
        public int Cost;
        public bool NeedTarget;

        public virtual SkillDamageType GetSkillDamageType()
        {
            throw new NotImplementedException();
        }

        public virtual void Release(UnitEntity targetUnit)
        {
            throw new NotImplementedException();
        }
    }
}
