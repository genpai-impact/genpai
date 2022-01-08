namespace Genpai
{
    class ProcessRoundEnd : IProcess
    {
        private static ProcessRoundEnd roundEndProcess = new ProcessRoundEnd();
        private ProcessRoundEnd()
        {
        }
        public static ProcessRoundEnd GetInstance()
        {
            return roundEndProcess;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public void Run()
        {
            GameContext.ChangePlayer();
            GameContext.processManager.Next();
        }
    }
}
