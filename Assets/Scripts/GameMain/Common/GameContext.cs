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

        }

        private void Start()
        {
            NormalProcessManager.Instance.Start();
        }

        /// <summary>
        /// 变更当前行动的玩家
        /// </summary>
        public static void ChangePlayer()
        {
            if (CurrentPlayer.Equals(Player1))
            {
                CurrentPlayer = Player2;
                return;
            }
            CurrentPlayer = Player1;
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
