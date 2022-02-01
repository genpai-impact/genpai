using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class ChangePlayer : MonoBehaviour, IMessageSendHandler
    {

        public bool state = true;
        public GameObject player1;
        public GameObject player2;

        private void Awake()
        {
            /*player1.GetComponent<Canvas>().enabled = true;
            player2.GetComponent<Canvas>().enabled = false;*/
            /*player1.SetActive(true);
            player2.SetActive(false);*/
            player1.transform.position= new Vector3(0, 9.5f, -9.13397503f);
            player2.transform.position= new Vector3(1, 9.5f, -9.13397503f);
            InitTrigger();
        }

        /// <summary>
        /// 初始化鼠标事件触发器
        /// </summary>
        public void InitTrigger()
        {
            // 将自身方法注册为UnityAction
            

            

            UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyOnMouseDown);
            EventTrigger.Entry myBeginDrag = new EventTrigger.Entry();
            myBeginDrag.eventID = EventTriggerType.PointerClick;
            myBeginDrag.callback.AddListener(click);

            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
           
            trigger.triggers.Add(myBeginDrag);
        }

        /// <summary>
        /// 鼠标点击事件触发方法
        /// 攻击请求和目标选中
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDown(BaseEventData data)
        {
            if (!state)
            {
                /*player1.GetComponent<Canvas>().enabled=true;
                player2.GetComponent<Canvas>().enabled = false;*/
                player1.transform.position = new Vector3(1, 9.5f, -9.13397503f);
                player2.transform.position = new Vector3(0, 9.5f, -9.13397503f);
                state = true;
            }
            else {
                /* player1.GetComponent<Canvas>().enabled = false;
                 player2.GetComponent<Canvas>().enabled = true;*/
                player1.transform.position = new Vector3(0, 9.5f, -9.13397503f);
                player2.transform.position = new Vector3(1, 9.5f, -9.13397503f);
                state = false;
            }
            
        }

        /// <summary>
        /// 鼠标拖动事件触发方法
        /// 攻击选择需求
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDrag(BaseEventData data)
        {

            // TODO：设计攻击选择箭头

        }

        /// <summary>
        /// 鼠标拖动事件松开触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseAfterDrag(BaseEventData data)
        {
            // 若未进入攻击流程，则销毁选择箭头对象
            if (AttackManager.Instance.waitingTarget == null)
            {

            }
            // 完成召唤确认
            else
            {
                MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.AttackConfirm, AttackManager.Instance.waitingTarget);

            }
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch (areaCode)
            {
                case MessageArea.Attack:
                    switch (eventCode)
                    {
                        case MessageEvent.AttackEvent.AttackRequest:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.AttackEvent.AttackRequest, message as GameObject);
                            break;
                        case MessageEvent.AttackEvent.AttackConfirm:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.AttackEvent.AttackConfirm, message as GameObject);
                            break;
                    }
                    break;
            }
        }
    }
}