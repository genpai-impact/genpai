using System;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class AIController : MonoBehaviour, IMessageReceiveHandler
    {
        public bool usingAI;
        public BaseAI AI;
        public AIType AItype;

        public void Start()
        {
            GameContext.UsingAI = usingAI;
            if (usingAI == true)
            {
                //利用反射使用string做类型名？
                if (AItype == AIType.SimpleAI) { AI = new SimpleAI(AItype, GameContext.Player2); }
                else if (AItype == AIType.FoolAI) { AI = new FoolAI(AItype, GameContext.Player2); }
                else
                {
                    //异常处理
                }

                Subscribe();
            }
        }

        public void AIAction(GenpaiPlayer _Player)
        {
            if (AI.Player == null) { AI.Player = _Player; }
            AI.CharaStrategy();
            AI.MonsterStrategy();
            AI.AttackStrategy();
                
            AI.EndRound();
        }

        public void Subscribe()
        {
            // 订阅AI操作请求
            MessageManager.Instance.GetManager(MessageArea.AI).Subscribe<GenpaiPlayer>(MessageEvent.AIEvent.AIAction, AIAction);
        }

    }
}