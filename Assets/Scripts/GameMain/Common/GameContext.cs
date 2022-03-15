using UnityEngine;

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
            if (CurrentPlayer.Equals(Player1))
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }

        }

        public static void ChangeLocalPlayer()
        {
            if (LocalPlayer.Equals(Player1))
            {
                LocalPlayer = Player2;
            }
            else
            {
                LocalPlayer = Player1;
            }
            GameObject.Find("GameManager").GetComponent<ChangePlayer>().ChangeLocalPlayer();
            // Debug.Log("Local Player is: " + LocalPlayer.playerSite);
        }

        /// <summary>
        /// 用于当前玩家
        /// </summary>
        /// <returns></returns>
        public GenpaiPlayer GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        /// <summary>
        /// 根据阵营获取玩家
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public GenpaiPlayer GetPlayerBySite(BattleSite site)
        {
            if (site == BattleSite.P1)
            {
                return Player1;
            }
            else if (site == BattleSite.P2)
            {
                return Player2;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return $"{{{nameof(Player1)}={Player1}, {nameof(Player2)}={Player2}, {nameof(TheBoss)}={TheBoss}, {nameof(CurrentPlayer)}={CurrentPlayer}}}";
        }
    }
}
