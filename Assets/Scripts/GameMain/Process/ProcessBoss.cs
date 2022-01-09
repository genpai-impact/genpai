using Messager;

namespace Genpai
{
    /// <summary>
    /// boss行动流程
    /// 技能释放需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d97da5e8d5a0003fbaa446
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
