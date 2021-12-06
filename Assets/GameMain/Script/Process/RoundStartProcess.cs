using System;

namespace Genpai
{
    /// <summary>
    /// 回合开始时需要执行的操作
    /// </summary>
    class RoundStartProcess : BaseRoundProcess
    {
        private static RoundStartProcess roundStartProcess = new RoundStartProcess();
        private RoundStartProcess()
        {
        }
        public static RoundStartProcess GetInstance()
        {
            return roundStartProcess;
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            // 抽牌
            GameContext.CurrentPlayer.DrawCard();
            // 结算状态
            RoundStart(GameContext.CurrentPlayer.BattlegroundBuckets);
            GameContext.processManager.Next();
        }
    }
}
