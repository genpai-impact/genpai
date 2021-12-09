namespace Genpai
{
    /// <summary>
    /// 回合进行中需要进行的操作
    /// </summary>
    class RoundProcess : IProcess
    {
        private static RoundProcess roundProcess = new RoundProcess();
        private RoundProcess()
        {
        }
        public static RoundProcess GetInstance()
        {
            return roundProcess;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public void Run()
        {
            GameContext.IsOperable = true;
            // 此时玩家可以进行操作，玩家操作完之后，调用NormalProcessManager的EndRound()进行下一个流程
            if (GameContext.CurrentPlayer.PlayerType == PlayerType.AI)
            {
                GameContext.CurrentPlayer.AI.Run();
            }
        }
    }
}
