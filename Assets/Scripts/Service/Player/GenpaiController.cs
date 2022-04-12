using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Genpai
{
    /// <summary>
    /// 原牌控制者
    /// </summary>
    public class GenpaiController
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
        /// 当前玩家是否可进行操作
        /// </summary>
        public bool IsOperable
        {
            get;
            set;
        }

        /// <summary>
        /// 开始控制者的回合
        /// </summary>
        public void StartRound()
        {
            IsOperable = true;
        }

        /// <summary>
        /// 结束控制者的回合
        /// </summary>
        public void EndRound()
        {
            if (!IsOperable)
            {
                return;
            }
            IsOperable = false;
            GameContext.processManager.EndRound();
        }
    }
}

