using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 战场上的格子
    /// </summary>
    public class BattlegroundBucket : IUnitCarryTargetable, ISkillTargetable
    {
        /// <summary>
        /// 这个格子上的作战单位
        /// </summary>
        public BaseUnit Unit;

        /// <summary>
        /// 这个格子上的作战单位
        /// </summary>
        public List<BattlegroundBucket> Nearby{
            get;set;
        }

        /// <summary>
        /// 查询格子是否持有作战单位
        /// </summary>
        public bool HaveUnit(){
            return Unit != null;
        }
        
        /// <summary>
        /// 获取相邻格子
        /// </summary>
        public List<BattlegroundBucket> AOENearby(){
            return Nearby;
        }

        /// <summary>
        /// 格子绑定作战单位
        /// 声明IUnitCarryTargetable中承载函数
        /// </summary>
        /// <param name="unit">作战单位</param>
        public void SetUnit(ref BaseUnit unit)
        {
            if (HaveUnit())
            {
                return;
            }
            Unit = unit;
        }
        
        /// <summary>
        /// 对格子上单位造成技能效果
        /// TODO: 引入技能实现类
        /// </summary>
        /// <param name="source">技能实现类</param>
        public void TakeSkill()
        {
            if(Unit == null)
            {
                return;
            }
            // Unit.TakeDamage();
        }
    }
}
