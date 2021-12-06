namespace Genpai
{
    class GameStartProcess : IProcess
    {
        private static GameStartProcess gameStartProcess = new GameStartProcess();
        private GameStartProcess()
        {
        }
        public static GameStartProcess GetInstance()
        {
            return gameStartProcess;
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
