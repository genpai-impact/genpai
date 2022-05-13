using Messager;

namespace Genpai
{
    /// <summary>
    /// 回合进行中需要进行的操作
    /// </summary>
    class ProcessRound : IProcess
    {
        private static readonly ProcessRound RoundProcess = new ProcessRound();
        private ProcessRound()
        {
        }

        public const string Name = "Round";

        public static ProcessRound GetInstance()
        {
            return RoundProcess;
        }

        public string GetName()
        {
            return Name;
        }
        public void Run()
        {
            GameContext.CurrentPlayer.GenpaiController.StartRound();
            if (GameContext.CurrentPlayer == GameContext.Player2)
            {
                MessageManager.Instance.Dispatch(MessageArea.AI, MessageEvent.AIEvent.AIAction, GameContext.CurrentPlayer);
            }

            // 回合自动结束 or 点击回合结束按钮
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}
