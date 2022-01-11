using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messager
{
    /// <summary>
    /// 区分消息作用域
    /// </summary>
    public enum MessageArea
    {
        UI,             // UI更新
        Process,        // 流程事件
        Behavior,       // 玩家交互
        Unit,           // 单位事件
        Effect,         // 效果事件

    }

    /// <summary>
    /// 所有消息集合
    /// 可以考虑修正为String形式
    /// </summary>
    public class MessageEvent
    {
        /// <summary>
        /// UI更新消息
        /// </summary>
        public class UIEvent
        {
            public const int RefreshCard = 0;
        }
        /// <summary>
        /// 游戏进程事件
        /// </summary>
        public class ProcessEvent
        {
            public const int OnGameStart = 0;
            public const int OnRoundStart = 1;
            public const int OnRound = 2;
            public const int OnRoundEnd = 3;
            public const int OnBossStart = 4;
            public const int OnGameEnd = 5;
        }
    }

}

