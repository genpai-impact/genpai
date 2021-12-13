using System.Collections.Generic;
using System;

namespace Genpai
{
    /// <summary>
    /// 技能类，通过技能ID，查表创造技能实现
    /// </summary>
    public class Skill
    {

        /// <summary>
        /// 技能来源
        /// </summary>
        public ISkillTargetable Source{
            get;set;
        }

        /// <summary>
        /// 技能目标
        /// </summary>
        public ISkillTargetable Target{
            get;set;
        }

        /// <summary>
        /// 技能ID
        /// </summary>
        public int SkillID{
            get;set;
        }

        /// <summary>
        /// 技能关键字
        /// </summary>
        public string SkillKey{
            get;set;
        }

        /// <summary>
        /// 技能三元组（实现id，作用目标，重复次数）
        /// </summary>
        public List<Tuple<int,SkillTargetEnum,int>> SkillActArgs{
            get;set;
        }

        /// <summary>
        /// 通过技能三元组列表创建的施法列表对象
        /// </summary>
        public List<SkillAct> SkillActList{
            get;set;
        }

        /// <summary>
        /// 技能构造函数
        /// </summary>
        /// <param name="skillID">待创建技能id</param>
        /// <param name="source">施法来源（用于实现自身、阵营回复等技能）</param>
        /// <param name="target">施法目标</param>
        public Skill(int skillID, ISkill source, ISkillTargetable target){
            if(source is BaseUnit)
            {
                //待朝BaseUnit类中补充GetBucket方法
                //Source = source.GetBucket();
            }
            else 
            {
                Source = null;
            }
            Target = target;
            SkillID = skillID;
            
        }

        /* 技能类参考JSON数据结构，详见文档
        {
            "SkillID":1
            "SkillKey":"Keqing_Burst",
            "SkillName":"天街巡游",
            "SkillAct":[
                {
                    "ActID":1,
                    "target":TARGET, //表示技能释放目标
                    "repeat":1
                },
                {
                    "ActID":2,
                    "target":NULL, 
                    "repeat":7
                }
            ]
        },
        */ 
        /// <summary>
        /// 通过读取数据库构造施法队列
        /// </summary>
        public void CreateSkillActList(){
            
        }
        /// <summary>
        /// 通过施法列表实现技能释放过程
        /// </summary>
        public void ActSkill(){
            foreach (SkillAct act in SkillActList){
                act.Effect();
            }
        }
    }
}