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
    /// 消息事件集合
    /// （可以考虑修正为String形式）
    /// </summary>
    public class MessageEvent
    {
        /// <summary>
        /// UI更新消息
        /// </summary>
        public class UIEvent
        {
            public const string RefreshCard = "RefreshCard";
        }
        /// <summary>
        /// 游戏进程事件
        /// </summary>
        public class ProcessEvent
        {
            public const string OnGameStart = "OnGameStart";
            public const string OnRoundStart = "OnRoundStart";
            public const string OnRoundEnd = "OnRoundEnd";
            public const string OnBossStart = "OnBossStart";
            public const string OnGameEnd = "OnGameEnd";
        }
        public class ContextEvent
        {
            public const string OnBossHPReach75 = "OnBossHPReach75";
        }

    }

}

