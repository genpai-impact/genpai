using Messager;

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
        /// <summary>
        /// 起手英雄数量
        /// </summary>
        private const int _startHeroCount = 2;
        private ProcessGameStart()
        {
        }
        public static string NAME = "GameStart";
        public static ProcessGameStart GetInstance()
        {
            return gameStartProcess;
        }

        public string GetName()
        {
            return NAME;
        }

        public void Run()
        {
            for (int i = 0; i < _startHeroCount; i++)
            {
                GameContext.Player1.CardDeck.DrawHero();
                GameContext.Player2.CardDeck.DrawHero();
            }
            for (int i = 0; i < _startCardCount; i++)
            {
                GameContext.Player1.CardDeck.DrawCard();
                GameContext.Player2.CardDeck.DrawCard();
            }
            GameContext.processManager.Next();
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}
