namespace Genpai
{
    class BossProcess : IProcess
    {
        private static BossProcess bossProcess = new BossProcess();
        private BossProcess()
        {
        }
        public static BossProcess GetInstance()
        {
            return bossProcess;
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
