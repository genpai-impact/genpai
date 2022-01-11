using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 攻击管理器，受理攻击请求
    /// 参考SummonManager同BattleFieldManager交互
    /// 攻击管理器确认攻击事件后生成伤害类，并通过EffectManager造成伤害
    /// </summary>
    public class AttackManager : MonoSingleton<AttackManager>, IMessageHandler
    {
        private GameObject waitingUnit;
        public bool attackWaiting;
        public PlayerID waitingPlayer;


        public void AttackRequest(GameObject _sourceUnit)
        {
            if (true)
            {
                waitingPlayer = _sourceUnit.GetComponent<Unit>().owner;
                waitingUnit = _sourceUnit;
                List<bool> attackHoldList = BattleFieldManager.Instance.CheckAttackable(waitingPlayer);
            }

        }


        public void AttackConfirm(GameObject _targetUnit)
        {
            Attack(waitingUnit, _targetUnit);
        }


        public void Attack(GameObject _sourceUnit, GameObject _targetUnit)
        {

        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }
    }
}