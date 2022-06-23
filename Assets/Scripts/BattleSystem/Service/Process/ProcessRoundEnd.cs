
using BattleSystem.Service.Common;
using Utils.Messager;

namespace BattleSystem.Service.Process
{
    class ProcessRoundEnd : IProcess
    {
        private static readonly ProcessRoundEnd RoundEndProcess = new ProcessRoundEnd();
        private ProcessRoundEnd()
        {
        }
        public static ProcessRoundEnd GetInstance()
        {
            return RoundEndProcess;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            return "RoundEnd";
        }
        public void Run()
        {
            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundEnd, GameContext.CurrentPlayer.playerSite);
            // 更换当前回合玩家
            GameContext.ChangeCurrentPlayer();
            // 更换本地操作玩家
            if (GameContext.UsingAI == false)
            {
                GameContext.ChangeLocalPlayer();
            }
            GameContext.ProcessManager.Next();
        }
    }
}
