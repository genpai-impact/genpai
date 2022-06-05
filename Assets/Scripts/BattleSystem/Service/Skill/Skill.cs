using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.effect;


namespace Genpai
{
    /// <summary>
    /// 新的Skill类，主要用于存EffectConstructor列表
    /// </summary>
    public class Skill
    {
        public int SkillId { get; }

        /// <summary>
        /// 消耗的MP量
        /// </summary>
        public int Cost { get; }
        
        /// <summary>
        /// 是否是主动技能
        /// <para>此属性用于判断释放之后是否扣除MP</para>
        /// </summary>
        public bool IsErupt { get; }

        /// <summary>
        /// Effect属性列表
        /// </summary>
        public List<EffectConstructProperties> EffectConstructorList { get; } = new List<EffectConstructProperties>();

        public Skill(int id, int cost, bool isErupt, List<EffectConstructProperties> effetConstructors)
        {
            SkillId = id;
            Cost = cost;
            IsErupt = isErupt;
            EffectConstructorList = effetConstructors;
        }
    }
}