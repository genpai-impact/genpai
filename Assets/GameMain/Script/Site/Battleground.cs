
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 维护战场信息
    /// </summary>
    public class Battleground
    {
        private static Battleground battleground = new Battleground();
        private Battleground()
        {
        }
        public static Battleground GetInstance()
        {
            return battleground;
        }

        public Boss boss
        {
            get;
            set;
        }

        public List<BattlegroundBucket> BossBuckets
        {
            get;
            set;
        }
    }
}
