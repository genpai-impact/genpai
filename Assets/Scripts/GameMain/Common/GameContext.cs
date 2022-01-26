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

        public override string ToString()
        {
            return $"{{{nameof(Player1)}={Player1}, {nameof(Player2)}={Player2}, {nameof(TheBoss)}={TheBoss}, {nameof(CurrentPlayer)}={CurrentPlayer}}}";
        }
    }
}
