﻿using UnityEngine;

namespace Genpai
{

    /// <summary>
    /// 存储整个游戏的上下文信息
    /// </summary>
    public partial class GameContext
    {

        public void Init()
        {
            // todo 未来选关之后，用选关选的那个关卡信息
            MissionConfig = new MissionConfig();
        }

        /// <summary>
        /// 变更当前行动的玩家
        /// </summary>
        public static void ChangeCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer.Equals(Player1) ? Player2 : Player1;
        }

        public static void ChangeLocalPlayer()
        {
            LocalPlayer = LocalPlayer.Equals(Player1) ? Player2 : Player1;
            GameObject.Find("GameManager").GetComponent<ChangePlayer>().ChangeLocalPlayer();
            // Debug.Log("Local Player is: " + LocalPlayer.playerSite);
        }

        /// <summary>
        /// 用于当前玩家
        /// </summary>
        /// <returns></returns>
        public static GenpaiPlayer GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        /// <summary>
        /// 返回玩家1
        /// </summary>
        /// <returns></returns>
        public static GenpaiPlayer GetPlayer1()
        {
            return Player1;
        }

        /// <summary>
        /// 返回玩家2
        /// </summary>
        /// <returns></returns>
        public static GenpaiPlayer GetPlayer2()
        {
            return Player2;
        }

        /// <summary>
        /// 根据阵营获取玩家
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public static GenpaiPlayer GetPlayerBySite(BattleSite site)
        {
            return site switch
            {
                BattleSite.P1 => Player1,
                BattleSite.P2 => Player2,
                _ => null
            };
        }

        public override string ToString()
        {
            return $"{{{nameof(Player1)}={Player1}, {nameof(Player2)}={Player2}, {nameof(TheBoss)}={TheBoss}, {nameof(CurrentPlayer)}={CurrentPlayer}}}";
        }
    }
}
