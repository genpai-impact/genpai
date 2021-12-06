
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 作战单位
    /// </summary>
    public interface IUnits : ISkillTargetable
    {
        public List<BaseBuff> GetBuffList();
    }
}
