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
        public static ProcessRound GetInstance()
        {
            return roundProcess;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public void Run()
        {
            GameContext.CurrentPlayer.GenpaiController.StartRound();
        }
    }
}
