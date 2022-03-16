using Messager;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// boss行动流程
    /// </summary>
    class ProcessBoss : IProcess
    {
        private int round = 0;

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
            round++;
            // boss第一回合不行动，产品需求如此
            if (round > 3 && GameContext.TheBoss.ActionState[UnitState.SkillUsing])
            {
                GameContext.BossComponent.AddMP();
                GameContext.BossComponent.Skill();
            }
            GameContext.processManager.Next();
        }
    }
}
