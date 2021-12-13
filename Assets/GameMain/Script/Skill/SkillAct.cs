namespace Genpai
{
    /// <summary>
    /// 技能实现类，通过实现ID造成效果
    /// </summary>
    public class SkillAct
    {
        /// <summary>
        /// 实现编号
        /// </summary>
        public int ActID{
            get;
        }
        
        /// <summary>
        /// 实现目标
        /// </summary>
        public ISkillTargetable Target{
            get;set;
        }
        

        /// <summary>
        /// 实现随机性
        /// 查找目标所指阵营，并随机选择位置作用
        /// </summary>
        public SkillTargetRandomEnum targetRandom{
            get;set;
        }

        /// <summary>
        /// 实现类型枚举（选择创造伤害、回复、buff对象）
        /// </summary>
        public SkillEffectTypeEnum effectType{
            get;set;
        }

        public SkillAct(int actID,ISkillTargetable target){
            ActID = actID;
            Target = target;
            CreateSkillAct();
        }

        /// <summary>
        /// 查表创建技能
        /// </summary>        
        public void CreateSkillAct(){
            
        }
        public void Effect(){
            
        }
    }
}