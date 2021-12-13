using System.Collections.Generic;

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
        /// 实现目标(暂存)
        /// </summary>
        public ISkillTargetable Target{
            get;set;
        }

        /// <summary>
        /// 实现目标(列表储存方便实现AOE)
        /// </summary>
        public List<ISkillTargetable> TargetList{
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
            TargetList.Add(target);
            // TODO：解析表格获取参数
            GetTarget();
            CreateAct();
        }

        /// <summary>
        /// 获取目标（根据当前目标、AOE类型、随机性类型获取）
        /// </summary>
        public void GetTarget(){
            if(targetRandom == SkillTargetRandomEnum.RANDOM)
            {
                //TODO: BattleGround类获取目标所在场地所有格子，并随机
            }
            else
            {
                //TODO: 根据三种AOE类型获取目标队列并加入Target
            }
        }
        /// <summary>
        /// 查表创建技能
        /// </summary>   
        public void CreateAct(){
            //TODO: 通过枚举类型创建不同游戏对象（主要为伤害、回复、buff）
        }
        public void Effect(){
            foreach(ISkillTargetable target in TargetList){
                //对每个目标使用Act对象
                target.TakeSkill();
            }
        }
    }
}