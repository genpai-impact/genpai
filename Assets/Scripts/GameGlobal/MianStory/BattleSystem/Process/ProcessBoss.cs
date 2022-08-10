using BattleSystem.Service.Common;
using BattleSystem.Service.Player;
using BattleSystem.Service.Unit;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utils.Messager;

namespace BattleSystem.Service.Process
{
    /// <summary>
    /// boss行动流程
    /// </summary>
    class ProcessBoss : IProcess
    {
        private int _round = 0;

       

        private static readonly ProcessBoss BossProcess = new ProcessBoss();
        private ProcessBoss()
        {
        }
        public static ProcessBoss GetInstance()
        {
            return BossProcess;
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
            _round++;

            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundStart, BattleSite.Boss);

            // boss第一回合不行动，需求如此
            if (_round > 3 && GameContext.TheBoss.ActionState[UnitState.SkillUsing])
            {
                GameContext.TheBoss.AddMP();
                GameContext.TheBoss.Skill();
            }

            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnRoundEnd, BattleSite.Boss);

            GameContext.ProcessManager.Next();
            //_mono.StartCoroutine(NextAfterAllAnimationOver(Next()));

        }

       /* public bool IsFinish() { 
            
        }*/

        private UnityAction Next() {
            GameContext.ProcessManager.Next();
            return null;
        }
    }
}
