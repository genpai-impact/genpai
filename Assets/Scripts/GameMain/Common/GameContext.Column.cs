using UnityEngine;

namespace Genpai
{

    /// <summary>
    /// 存储整个游戏的上下文信息
    /// 本文件只存字段
    /// </summary>
    public partial class GameContext : Singleton<GameContext>
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
        /// Boss
        /// 暂存组件对象方便访问
        /// </summary>
        public static UnitEntity TheBoss
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
        /// 本地玩家/界面操控玩家
        /// </summary>
        public static GenpaiPlayer LocalPlayer
        {
            get;
            set;
        }
        /// <summary>
        /// 上一玩家阵营
        /// 主要用于Boss获取
        /// （草率）
        /// </summary>
        public static BattleSite PreviousPlayerSite
        {
            get
            {
                if (CurrentPlayer.playerSite == BattleSite.P1) return BattleSite.P2;
                else return BattleSite.P1;
            }
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
