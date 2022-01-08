namespace Genpai
{

    /// <summary>
    /// 存储整个游戏的上下文信息
    /// </summary>
    public partial class GameContext : MonoSingleton<GameContext>
    {
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
    }
}
