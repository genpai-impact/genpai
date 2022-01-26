using Messager;

namespace Genpai
{
    /// <summary>
    /// boss行动流程
    /// </summary>
    class ProcessBoss : IProcess
    {
        private static ProcessBoss bossProcess = new ProcessBoss();
        private ProcessBoss()
        {
        }
        public static ProcessBoss GetInstance()
        {
            return bossProcess;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            return "RoundBoss";
        }
        public void Run()
        {
            // boss第一回合不行动，产品需求如此
            if (GameContext.CurrentPlayer.CurrentRound > 1)
            {
                // GameContext.BattleField.boss.OnBossStart();
            }
            GameContext.processManager.Next();
        }
    }
}
