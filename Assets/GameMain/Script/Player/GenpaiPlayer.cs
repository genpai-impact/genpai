using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 游戏中的玩家信息
    /// </summary>
    public class GenpaiPlayer
    {
        /// <summary>
        /// 控制者
        /// </summary>
        public GenpaiController GenpaiController
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家的卡组
        /// </summary>
        public CardDeck CardDeck
        {
            get;
            set;
        }

        /// <summary>
        /// 当前是第几回合
        /// </summary>
        public int CurrentRound
        {
            get;
            set;
        }
    }
}
