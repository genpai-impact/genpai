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
                if (bucket == null || bucket.units == null || bucket.units.GetBuffList() == null)
                {
                    continue;
                }
                foreach (BaseBuff buff in bucket.units.GetBuffList())
                {
                    buff.OnRoundStart();
                }
            }
        }
    }
}
