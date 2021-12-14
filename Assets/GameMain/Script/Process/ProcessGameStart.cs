namespace Genpai
{
    /// <summary>
    /// 游戏开始时需要做的事
    /// </summary>
    class ProcessGameStart : IProcess
    {
        private static ProcessGameStart gameStartProcess = new ProcessGameStart();
        /// <summary>
        /// 起始手牌数量
        /// </summary>
        private const int _startCardCount = 4;
        private ProcessGameStart()
        {
        }
        public static ProcessGameStart GetInstance()
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
                GameContext.Player1.CardDeck.DrawCard();
                GameContext.Player2.CardDeck.DrawCard();
            }
            GameContext.processManager.Next();
        }
    }
}
