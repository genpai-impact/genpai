namespace Genpai
{
    class GameEndProcess : IProcess
    {
        private static GameEndProcess gameEndProcess = new GameEndProcess();
        private GameEndProcess()
        {
        }
        public static GameEndProcess GetInstance()
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
