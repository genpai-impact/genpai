﻿using BattleSystem.Controller.Bucket;
using BattleSystem.Service.Mission;
using BattleSystem.Service.Player;
using BattleSystem.Service.Process;
using BattleSystem.Service.Unit;
using cfg.level;
using Utils;

namespace BattleSystem.Service.Common
{

    /// <summary>
    /// 存储整个游戏的上下文信息
    /// 本文件只存字段
    /// </summary>
    public partial class GameContext : Singleton<GameContext>
    {

        /// <summary>
        /// 关卡信息
        /// </summary>
        public static MissionConfig MissionConfig
        {
            get;
            set;
        }

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
        /// 是否使用AI
        /// </summary>
        public static bool UsingAI
        {
            get;
            set;
        }

        /// <summary>
        /// 切换角色CD
        /// </summary>
        public static int CharaCd
        {
            get;
            set;
        }

        /// <summary>
        /// Boss
        /// 暂存组件对象方便访问
        /// </summary>
        public static Boss TheBoss
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
        public static BattleSite PreviousPlayerSite => CurrentPlayer.playerSite == BattleSite.P1 ? BattleSite.P2 : BattleSite.P1;

        /// <summary>
        /// 战场信息
        /// </summary>
        public static BucketEntityManager BattleField = BucketEntityManager.Instance;

        /// <summary>
        /// 流程管理
        /// </summary>
        public static readonly NormalProcessManager ProcessManager = NormalProcessManager.Instance;
    }
}
