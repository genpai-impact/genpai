namespace Genpai
{
    class GameStartProcess : IProcess
    {
        private static GameStartProcess gameStartProcess = new GameStartProcess();
        /// <summary>
        /// 起始手牌数量
        /// </summary>
        private const int _startCardCount = 4;
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
            for (int i = 0; i < _startCardCount; i++)
            {
                GameContext.Player1.DrawCard();
                GameContext.Player2.DrawCard();
            }
            GameContext.processManager.Next();
        }
    }
}
