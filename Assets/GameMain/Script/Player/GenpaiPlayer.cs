using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 游戏中的玩家信息
    /// </summary>
    public class GenpaiPlayer
    {
        /// <summary>
        /// 玩家类型
        /// </summary>
        public PlayerType PlayerType
        {
            get;
            set;
        }

        /// <summary>
        /// AI
        /// </summary>
        public BaseGenpaiAI AI
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家的卡组
        /// </summary>
        public CardDeck cardDeck
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家的牌堆
        /// </summary>
        public CardLibrary cardLibrary
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家的手牌
        /// </summary>
        public CardHand cardHand
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家已经使用过的牌
        /// </summary>
        public CardUsed cardUsed
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家的场地
        /// </summary>
        public List<BattlegroundBucket> BattlegroundBuckets
        {
            get;
            set;
        }

        /// <summary>
        /// 抽牌
        /// </summary>
        public void DrawCard()
        {

        }
    }
}
