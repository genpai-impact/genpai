using Messager;
using System;

namespace Genpai
{
    /// <summary>
    /// 回合开始时需要执行的操作
    /// </summary>
    class ProcessRoundStart : IProcess
    {

        private const int _CardsPerRound = 1;
        private static ProcessRoundStart roundStartProcess = new ProcessRoundStart();
        private ProcessRoundStart()
        {
        }
        public static ProcessRoundStart GetInstance()
        {
            return roundStartProcess;
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            for(int i = 0; i < _CardsPerRound; i++)
            {
                 GameContext.CurrentPlayer.CardDeck.DrawCard();
            }
            GameContext.processManager.Next();
        }
    }
}
