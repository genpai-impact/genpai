
using Messager;

namespace Genpai
{
    class ProcessRoundEnd : IProcess
    {
        private static ProcessRoundEnd roundEndProcess = new ProcessRoundEnd();
        private ProcessRoundEnd()
        {
        }
        public static ProcessRoundEnd GetInstance()
        {
            return roundEndProcess;
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
            // 更换当前回合玩家
            GameContext.ChangeCurrentPlayer();
            // 更换本地操作玩家
            if (GameContext.usingAI == false)
            {
                GameContext.ChangeLocalPlayer();
            }
            GameContext.processManager.Next();
        }
    }
}
