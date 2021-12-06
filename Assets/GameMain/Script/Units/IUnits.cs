
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 作战单位
    /// </summary>
    public interface IUnits : ISkillTargetable, IProcessHandle
    {
        public List<BaseBuff> GetBuffList();
        public List<ISkill> GetSkillList();
        public void OnRoundStart();
    }
}
