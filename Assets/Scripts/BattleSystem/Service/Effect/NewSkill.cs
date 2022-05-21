using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg.effect;


namespace Genpai
{
    /// <summary>
    /// 新的Skill类，主要用于存EffectConstructor列表
    /// </summary>
    public class NewSkill
    {
        public int SkillId { get; }

        /// <summary>
        /// 消耗的MP量
        /// </summary>
        public int Cost { get; }

        /// <summary>
        /// Effect构造器列表
        /// </summary>
        public List<EffectConstructProperties> EffectConstructorList { get; } = new List<EffectConstructProperties>();

        public NewSkill(int id, int cost, List<EffectConstructProperties> effetConstructors)
        {
            SkillId = id;
            Cost = cost;
            EffectConstructorList = effetConstructors;
        }
    }
}