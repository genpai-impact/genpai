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
            GameContext.usingAI = usingAI;
            if (usingAI == true)
            {
                //���÷���ʹ��string����������
                if (AItype == AIType.SimpleAI) { AI = new SimpleAI(AItype, GameContext.Player2); }
                else if (AItype == AIType.FoolAI) { AI = new FoolAI(AItype, GameContext.Player2); }
                else
                {
                    //�쳣����
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
            // ����AI��������
            MessageManager.Instance.GetManager(MessageArea.AI).Subscribe<GenpaiPlayer>(MessageEvent.AIEvent.AIAction, AIAction);
        }

    }
}