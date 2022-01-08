using System;

namespace Genpai
{
    /// <summary>
    /// 回合开始时需要执行的操作
    /// </summary>
    class ProcessRoundStart : IProcess
    {
        private static ProcessRoundStart roundStartProcess = new ProcessRoundStart();
        private ProcessRoundStart()
        {
        }
        public static ProcessRoundStart GetInstance()
        {
            return roundStartProcess;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            // GameContext.CurrentPlayer.CardDeck.DrawCard();
            GameContext.processManager.Next();
        }
    }
}
