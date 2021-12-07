
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 作战单位
    /// </summary>
    public interface IUnit : ISkillTargetable, IProcessHandle
    {
        /// <summary>
        /// 获得该单位身上的所有buff（除了元素
        /// </summary>
        /// <returns>该单位身上所有的buff</returns>
        public List<BaseBuff> GetBuffList();
        /// <summary>
        /// 获取该单位所有的技能
        /// </summary>
        /// <returns>该单位所有的技能</returns>
        public List<ISkill> GetSkillList();
        /// <summary>
        /// 获取该单位当前挂载的元素
        /// </summary>
        /// <returns>该单位当前挂载的元素</returns>
        public Element GetElement();
        /// <summary>
        /// 对该单位进行元素挂载
        /// </summary>
        /// <param name="element">需要挂载的新元素</param>
        public void AddElement(Element element);
    }
}
