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
        Round,          // 回合进行
        Behavior,       // 玩家交互
        Unit,
        Damage

    }

    /// <summary>
    /// 所有消息集合
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
        public class RoundEvent
        {

        }
        public class PlayerEvent
        {

        }
    }

}

