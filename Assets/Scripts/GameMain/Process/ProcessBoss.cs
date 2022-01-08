namespace Genpai
{
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
