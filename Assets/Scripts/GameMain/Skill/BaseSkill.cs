using System;

namespace Genpai
{
    public abstract class BaseSkill
    {
        public int ID;
        public string SkillName;
        public SkillType SkillType;
        public string SkillDesc;
        public int Cost;

        private UnitEntity _target;

        public void Release()
        {
            throw new NotImplementedException();
        }
    }
}
