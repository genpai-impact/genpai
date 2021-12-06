namespace Genpai
{
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
            throw new System.NotImplementedException();
        }
    }
}
