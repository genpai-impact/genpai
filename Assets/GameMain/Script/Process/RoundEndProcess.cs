namespace Genpai
{
    class RoundEndProcess : IProcess
    {
        private static RoundEndProcess roundEndProcess = new RoundEndProcess();
        private RoundEndProcess()
        {
        }
        public static RoundEndProcess GetInstance()
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
