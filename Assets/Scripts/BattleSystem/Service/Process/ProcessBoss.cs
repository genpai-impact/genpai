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

        /*//弈言不和开协程hh，除非改框架把主流程写入update函数里面，不然没有其他更好的方法了2022/7/28.追加：弃用，已加入主流程：NormalProcessManager 2022/7/28
        private IEnumerator NextAfterAllAnimationOver(UnityAction action)
        {
            while (!AnimationHandle.Instance.AllAnimationOver())
            {
                yield return null;
            }
            action.Invoke();
        }*/

        //private UnityEngine.MonoBehaviour _mono = GameObject.FindObjectOfType<MonoBehaviour>();

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
