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
        Context,        // 上下文事件
        Unit,           // 单位事件
        Card,           // 卡牌移动
        Attack,         // 攻击事件
        Magic,          // 魔法事件
        Summon,         // 召唤事件
        AI
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
            public const string AttackHighLight = "AttackHighLight";
            public const string SummonHighLight = "SummonHighLight";
            public const string ShutUpHighLight = "ShutUpHighLight";
        }
        /// <summary>
        /// 游戏进程事件
        /// </summary>
        public class ProcessEvent
        {
            public const string OnGameStart = "OnGameStart";
            public const string OnRoundStart = "OnRoundStart";
            public const string OnRound = "OnRound";
            public const string OnRoundEnd = "OnRoundEnd";
            public const string OnGameEnd = "OnGameEnd";
            public const string OnGameRestart = "OnGameRestart";
        }
        public class ContextEvent
        {
            public const string OnBossHPReach75 = "OnBossHPReach75";
            public const string OnBossHPReach50 = "OnBossHPReach50";
            public const string BossFall = "BossFall";
            public const string CharaFall = "CharaFall";
            public const string CharaDead = "CharaDead";
            public const string BossScoring = "BossScoring";
        }

        public class SummonEvent
        {
            public const string SummonRequest = "SummonRequest";
            public const string SummonConfirm = "SummonConfirm";
        }

        public class AttackEvent
        {
            public const string AttackRequest = "AttackRequest";
            public const string AttackConfirm = "AttackConfirm";
        }


        public class MagicEvent
        {
            public const string MagicRequest = "MagicRequest";
            public const string AttackConfirm = "MagicAttackConfirm";
            public const string CureConfirm = "CureConfirm";
            public const string BuffConfirm = "BuffConfirm";
        }
        public class AIEvent
        {
            public const string AIAction = "AIAction";
        }


    }

}

