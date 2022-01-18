namespace Genpai
{
   
    /// <summary>
    /// 存储整个游戏的上下文信息
    /// 本文件只存字段
    /// </summary>
    public partial class GameContext
    {
        /// <summary>
        /// 玩家1
        /// </summary>
        public static GenpaiPlayer Player1
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家2
        /// </summary>
        public static GenpaiPlayer Player2
        {
            get;
            set;
        }

        /// <summary>
        /// 当前是哪个玩家行动
        /// </summary>
        public static GenpaiPlayer CurrentPlayer
        {
            get;
            set;
        }

        /// <summary>
        /// 战场信息
        /// </summary>
        public static BattleFieldManager BattleField = BattleFieldManager.Instance;

        /// <summary>
        /// 流程管理
        /// </summary>
        public static NormalProcessManager processManager = NormalProcessManager.Instance;
    }
}
