using System;
using cfg.buff;
using cfg.effect;
using cfg.common;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

namespace Genpai
{
    public class Buff
    {
        public readonly int BuffId;
        public readonly string BuffName;
        
        // 基础Buff类型(决定Buff执行机理
        public readonly string BaseBuffName;
        public readonly cfg.buff.BaseBuff BaseBuff;
        
        // 是否可删除(受DelBuff影响)
        public bool DeleteAble => BaseBuff.Deleteable;
        // 是否可叠层
        public bool IncreaseAble => BaseBuff.Increaseable;
        
        // 是否有回合数(疑似与SelfDestruction冗余)
        public bool HaveLifeCycle => BaseBuff.HaveLifeCycle;
        
        // 是否主动触发
        public bool Initiative => BaseBuff.Initiative;
        public RoundTime InitiativeTime=> BaseBuff.InitiativeTime;
        // 是否自动销毁
        public bool SelfDestruction => BaseBuff.SelfDestruction;
        public RoundTime DestructionTime => BaseBuff.DestructionTime;
        
        
        // public bool IsWorking;  // 这个标识符应该由Buff管理吗?
        // 核心参数
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

        /// <summary>
        /// 通过BuffId和附加参数props(数值)对Buff进行初始化
        /// </summary>
        /// <param name="buffId">根据BuffId查表获取构造形式</param>
        /// <param name="props">构造参数(根据Buff自身特性决定重载为什么)</param>
        public Buff (int buffId, int props = default)
        {
            BuffItem buffItem = LubanLoader.tables.BuffItems.Get(buffId);
            BuffId = buffItem.Id;
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
                case BuffPropertiesOverrideable.All: // 不确定用不用得着
                case BuffPropertiesOverrideable.None:
                default:
                    break;
            }
        }
        
        public Buff (Buff buff)
        {
            BuffName = buff.BuffName;
            BaseBuffName = buff.BaseBuffName;
            BaseBuff = buff.BaseBuff;
            
            Storey = buff.Storey;
            LifeCycle = buff.LifeCycle;
            BuffAppendix = buff.BuffAppendix;
        }

        /// <summary>
        /// 时间点自减方法
        /// </summary>
        /// <returns>是否烂完</returns>
        public bool Destruction()
        {
            if (SelfDestruction) LifeCycle--;
            return LifeCycle <= 0;
        }
        
    }
}