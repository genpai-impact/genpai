using Messager;
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

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            return "RoundStart";
        }

        public void Run()
        {
            GameContext.CurrentPlayer.CurrentRound++;
            GameContext.CurrentPlayer.CardDeck.HandOutCard(1);

            // 发送回合开始消息
            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundStart, true);

            // message为当前回合所属Site
            GameContext.processManager.Next();
        }
    }
}
