namespace Genpai
{
    class RoundStartProcess : IProcess
    {
        private static RoundStartProcess roundStartProcess = new RoundStartProcess();
        private RoundStartProcess()
        {
        }
        public static RoundStartProcess GetInstance()
        {
            return roundStartProcess;
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
