
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 维护战场信息
    /// todo 移动到boss下
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

        public IBoss boss
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
