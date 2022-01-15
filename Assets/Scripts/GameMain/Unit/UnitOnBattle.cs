using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 单位战场行为
    /// </summary>
    public class UnitOnBattle : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {
        public Unit unit;

        /// <summary>
        /// 点击单位实现攻击请求与确认交互
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            // 检查AttackManager中攻击等待状态
            // 如果单位可行动且为己方回合
            if (true)
            {
                // 点击发起攻击请求
            }
            // 如果等待攻击确认且非己方单位
            else
            {
                // 点击发起攻击确认
            }
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}