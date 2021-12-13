using System;
using System.Collections.Generic;

namespace Genpai
{
    public abstract class BaseRoundProcess : IProcess
    {
        public abstract string GetName();
        public abstract void Run();

        /// <summary>
        /// 结清状态
        /// </summary>
        /// <param name="Player1Buckets">需要结清状态的某个势力的地盘</param>
        protected void RoundStart(List<BattlegroundBucket> Buckets)
        {
            foreach (BattlegroundBucket bucket in Buckets)
            {
                if (bucket == null || bucket.Unit == null)
                {
                    continue;
                }
                // 该棋盘上的某个作战单位在回合开始时的动作（例如角色回复能量
                // bucket.Unit.OnRoundStart();
                if (bucket.Unit.GetBuffList() == null)
                {
                    continue;
                }
                // 该棋盘上的某个作战单位的buff在回合开始时的动作（例如角色被挂了引燃
                foreach (BaseBuff buff in bucket.Unit.GetBuffList())
                {
                    buff.OnRoundStart();
                }
            }
        }
    }
}
