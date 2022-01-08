namespace Genpai
{
    class ProcessGameEnd : IProcess
    {
        private static ProcessGameEnd gameEndProcess = new ProcessGameEnd();
        private ProcessGameEnd()
        {
        }
        public static ProcessGameEnd GetInstance()
        {
            return gameEndProcess;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
