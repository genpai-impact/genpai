
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 作战单位
    /// </summary>
    // 个人认为单位不该直接拥有IProcess接口，而是将回合开始操作统一视为buff清算
    public class BaseUnit 
    {
        public List<BaseBuff> buffList{
            get;set;
        }
        public List<ISkill> skillList{
            get;set;
        }
        public Element selfElement{
            get;set;
        }

        /// <summary>
        /// 作战单位构造
        /// </summary>
        /// <param name="unitID">作战单位对应ID</param>
        public BaseUnit(int unitID){
            CreateModel();
            //TODO:创建三维所需变量，并从json表单中获取
        }

        /// <summary>
        /// 创建模型（返回什么）
        /// </summary>
        public void CreateModel(){}

        /// <summary>
        /// 攻击某一目标
        /// </summary>
        /// <param name="unit">被攻击的目标</param>
        public void Attack(BaseUnit unit){}

        /// <summary>
        /// 对该作战单位造成伤害
        /// </summary>
        /// <param name="damage">所造成的伤害的类型和数值</param>
        public void TakeDamage(ElementDamage damage){}

        /// <summary>
        /// 获得该单位身上的所有buff（除了元素
        /// </summary>
        /// <returns>该单位身上所有的buff</returns>
        public List<BaseBuff> GetBuffList(){
            return buffList;
        }

        /// <summary>
        /// 获取该单位所有的技能
        /// </summary>
        /// <returns>该单位所有的技能</returns>
        public List<ISkill> GetSkillList(){
            return skillList;
        }

        /// <summary>
        /// 获取该单位当前挂载的元素
        /// </summary>
        /// <returns>该单位当前挂载的元素</returns>
        public Element GetElement(){
            return selfElement;
        }

        /// <summary>
        /// 对该单位进行元素挂载
        /// </summary>
        /// <param name="element">需要挂载的新元素</param>
        public void AddElement(Element element){}

    }
}
