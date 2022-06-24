using BattleSystem.Controller;
using BattleSystem.Controller.Animator;
using BattleSystem.Controller.Animator.TargetAnimators;
using BattleSystem.Service.BattleField;
using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using Utils.Messager;

namespace BattleSystem.Service.Process
{
    /// <summary>
    /// 回合开始时需要执行的操作
    /// </summary>
    class ProcessRoundStart : IProcess
    {
        private static readonly ProcessRoundStart RoundStartProcess = new ProcessRoundStart();
        private ProcessRoundStart()
        {
        }
        public static ProcessRoundStart GetInstance()
        {
            return RoundStartProcess;
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
            CurrentPlayer.HandOutCard(GameContext.MissionConfig.RoundCardCount);
            CurrentPlayer.SubCharaCD();
            CurrentPlayer.CharaManager.CDRefresh();

            CurrentPlayer.Chara.AddMP();
            CurrentPlayer.CurrentRoundMonsterCount = 0;

            // 发送阵营信息
            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundStart, GameContext.CurrentPlayer.playerSite);

            UpdateUIOnField();

            // message为当前回合所属Site
            GameContext.ProcessManager.Next();
        }

        public void UpdateUIOnField()
        {
            AnimatorTimeStep animatorTimeStep = new AnimatorTimeStep();
            foreach (var item in BattleFieldManager.Instance.Buckets)
            {
                if (BattleFieldManager.Instance.CheckCarryFlag(item.Key) == true)
                {
                    animatorTimeStep.AddTargetAnimator(new UIAnimator(item.Value.unitCarry));
                }
            }
            AnimatorManager.Instance.InsertAnimatorTimeStep(animatorTimeStep);
        }
    }
}
