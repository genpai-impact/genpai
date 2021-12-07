
using System.Collections.Generic;

namespace Genpai
{
    public abstract class BaseHero : IUnit
    {
        public void AddElement(Element element)
        {
            throw new System.NotImplementedException();
        }

        public List<BaseBuff> GetBuffList()
        {
            throw new System.NotImplementedException();
        }

        public Element GetElement()
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
