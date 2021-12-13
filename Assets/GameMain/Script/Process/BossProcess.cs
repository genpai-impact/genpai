namespace Genpai
{
    class BossProcess : IProcess
    {
        private static BossProcess bossProcess = new BossProcess();
        private BossProcess()
        {
        }
        public static BossProcess GetInstance()
        {
            return bossProcess;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public void Run()
        {
            if (GameContext.CurrentRound > 0)
            {
                GameContext.Battleground.boss.GetSkillList();
                // GameContext.Battleground.boss.OnRoundStart();
                foreach (ISkill skill in GameContext.Battleground.boss.GetSkillList())
                {
                    // todo 随机对一个目标发射技能。
                }
            }
            GameContext.processManager.Next();
        }
    }
}
