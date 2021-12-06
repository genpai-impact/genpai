
using System.Collections.Generic;

namespace Genpai
{
    public abstract class BaseHero : IUnits
    {
        public List<BaseBuff> GetBuffList()
        {
            throw new System.NotImplementedException();
        }

        public List<ISkill> GetSkillList()
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundStart()
        {
            throw new System.NotImplementedException();
        }
    }
}
