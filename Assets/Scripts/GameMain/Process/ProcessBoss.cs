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

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public void Run()
        {
            if (GameContext.CurrentPlayer.CurrentRound > 0)
            {
                // GameContext.BattleField.boss.OnBossStart();
            }
            GameContext.processManager.Next();
        }
    }
}
