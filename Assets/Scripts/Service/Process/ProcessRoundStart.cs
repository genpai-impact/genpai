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
            GenpaiPlayer CurrentPlayer = GameContext.CurrentPlayer;
            CurrentPlayer.CurrentRound++;
            CurrentPlayer.HandOutCard(1);
            CurrentPlayer.SubCharaCD();
            CurrentPlayer.CharaManager.CDRefresh();

            CurrentPlayer.Chara.AddMP();
            CurrentPlayer.CurrentRoundMonsterCount = 0;
            // TODO：待修复发送阵营信息
            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundStart, GameContext.CurrentPlayer.playerSite);

            AnimatorManager.Instance.InsertAnimatorTimeStep(AnimatorGenerator.GenerateUITimeStep(CurrentPlayer.Chara));

            // message为当前回合所属Site
            GameContext.processManager.Next();
        }
    }
}
