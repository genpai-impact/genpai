using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位战场行为
    /// 主要为各种情景下点击交互的实现
    /// </summary>
    public class UnitOnBattle : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {

        /// <summary>
        /// 点击单位实现攻击请求与确认交互
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UnitEntity unit = GetComponent<UnitEntity>();

            // 位于玩家回合、选中己方单位、单位可行动
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner == GameContext.LocalPlayer &&
                unit.actionState == true)
            {
                if (!AttackManager.Instance.attackWaiting)
                {
                    // 向攻击管理器发布攻击请求
                }

            }

            // 位于玩家回合、选中敌方单位
            if (GameContext.CurrentPlayer == GameContext.LocalPlayer &&
                unit.owner != GameContext.LocalPlayer)
            {
                if (AttackManager.Instance.attackWaiting)
                {
                    // 向攻击管理器发布攻击确认
                }

                // 还有一个技能/魔法攻击的流程

            }

        }
        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(string eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}