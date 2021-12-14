
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 作战单位
    /// </summary>
    public abstract class BaseUnit : ISkillTargetable, IProcessHandle
    {
        /// <summary>
        /// 攻击某一目标
        /// </summary>
        /// <param name="unit">被攻击的目标</param>
        public abstract void Attack(BaseUnit unit);
        /// <summary>
        /// 对该作战单位造成伤害
        /// </summary>
        /// <param name="damage">所造成的伤害的类型和数值</param>
        public abstract void TakeDamage(ElementDamage damage);
        /// <summary>
        /// 获得该单位身上的所有buff（除了元素
        /// </summary>
        /// <returns>该单位身上所有的buff</returns>
        public abstract List<BaseBuff> GetBuffList();
        /// <summary>
        /// 获取该单位所有的技能
        /// </summary>
        /// <returns>该单位所有的技能</returns>
        public abstract List<ISkill> GetSkillList();
        /// <summary>
        /// 获取该单位当前挂载的元素
        /// </summary>
        /// <returns>该单位当前挂载的元素</returns>
        public abstract Element GetElement();
        /// <summary>
        /// 对该单位进行元素挂载
        /// </summary>
        /// <param name="element">需要挂载的新元素</param>
        public abstract void AddElement(Element element);

        public void OnGameStart()
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundStart()
        {
            throw new System.NotImplementedException();
        }

        public void OnRound()
        {
            throw new System.NotImplementedException();
        }

        public void OnRoundEnd()
        {
            throw new System.NotImplementedException();
        }

        public void OnBossStart()
        {
            throw new System.NotImplementedException();
        }

        public void OnGameEnd()
        {
            throw new System.NotImplementedException();
        }
    }
}
