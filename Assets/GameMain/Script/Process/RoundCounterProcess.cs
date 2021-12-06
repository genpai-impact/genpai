namespace Genpai
{
    class RoundCounterProcess : IProcess
    {
        private static RoundCounterProcess roundCounterProcess = new RoundCounterProcess();
        private RoundCounterProcess()
        {
        }
        public static RoundCounterProcess GetInstance()
        {
            return roundCounterProcess;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public void Run()
        {
            GameContext.CurrentRound++;
            GameContext.processManager.Next();
        }
    }
}
