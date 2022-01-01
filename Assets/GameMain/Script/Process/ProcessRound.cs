namespace Genpai
{
    /// <summary>
    /// 回合进行中需要进行的操作
    /// </summary>
    class ProcessRound : IProcess
    {
        private static ProcessRound roundProcess = new ProcessRound();
        private ProcessRound()
        {
        }

        public static string NAME = "Round";

        public static ProcessRound GetInstance()
        {
            return roundProcess;
        }

        public string GetName()
        {
            return NAME;
        }
        public void Run()
        {
            GameContext.CurrentPlayer.GenpaiController.StartRound();
        }
    }
}
