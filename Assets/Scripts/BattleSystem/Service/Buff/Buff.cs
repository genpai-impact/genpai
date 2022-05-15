using System;
using cfg.buff;
using cfg.effect;

namespace Genpai
{
    public class Buff
    {
        // 显示的Buff名称
        public readonly string BuffName;
        
        // 基础Buff类型(决定Buff执行机理
        public readonly string BaseBuffName;
        public readonly cfg.buff.BaseBuff BaseBuff;
        
        // 是否可删除(受DelBuff影响)
        public bool DeleteAble => BaseBuff.Deleteable;
        // 是否可层数(可叠加)
        public bool IncreaseAble => BaseBuff.Increaseable;
        // 是否有回合数
        public bool HaveLifeCycle => BaseBuff.HaveLifeCycle;
        
        // 是否主动触发
        public bool Initiative => BaseBuff.Initiative;
        public string InitiativeTime=> BaseBuff.InitiativeTime;
        // 是否自动销毁
        public bool SelfDestruction => BaseBuff.SelfDestruction;
        public string DestructionTime => BaseBuff.DestructionTime;
        
        public int Storey;
        public int LifeCycle;

        /// <summary>
        /// 用于描述补充特性
        /// For DamageReduce: Armor/Shield
        /// For StateEffect: Freeze/ElectroCharge
        /// For ElementBuff/DOT: Element
        /// 在Buff生效时读取
        /// </summary>
        public readonly string BuffAppendix;
        

        public Buff(int buffId, int props = default)
        {
            BuffItem buffItem = LubanLoader.tables.BuffItems.Get(buffId);
            BuffName = buffItem.BuffNameZh;

            BuffConstructProperties bcp = buffItem.ConstructProperties;
            
            BaseBuffName = bcp.BuffType;
            BaseBuff = LubanLoader.tables.BaseBuffItems.Get(BaseBuffName);

            // 初始化层数时间及特性
            Storey = bcp.Stories;
            LifeCycle = bcp.LifeCycles;
            BuffAppendix = bcp.ConstructInfo;
    
            // 是否默认构造
            if (props == default) return;

            switch (buffItem.OverrideProperties)
            {
                case BuffPropertiesOverrideable.Stories:
                    Storey = props;
                    break;
                case BuffPropertiesOverrideable.LifeCycles:
                    LifeCycle = props;
                    break;
                case BuffPropertiesOverrideable.All: // 回头补上
                case BuffPropertiesOverrideable.None:
                default:
                    break;
            }
        }
    }
}