using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{

    /// <summary>
    /// 单个卡牌管理器
    /// </summary>
    public class CardControler : MonoBehaviour, IMessageHandler
    {

        public float smooth = 2;    //平滑移动系数
        bool isMoveTo = false;      //移动控制器
        private Vector3 target;     //移动目标位置


        // Use this for initialization
        private void Awake()
        {
            Subscribe();

            InitTrigger();
        }



        // Update is called once per frame
        void Update()
        {

            if (isMoveTo)
            {
                Vector3 temp = Vector3.Lerp(this.transform.localPosition, target, Time.deltaTime * smooth);
                this.transform.localPosition = temp;

                //Debug.Log("moving");
                if (System.Math.Abs(transform.localPosition.x - target.x) <= 0.1)
                    isMoveTo = false;
            }
        }

        /// <summary>
        /// 监听事件响应方法
        /// </summary>
        /// <param name="data">监听事件传入消息</param>
        public void MoveTo(MoveToData data)
        {

            if (this.gameObject == data.gameObject)
            {
                // Debug.LogWarning(gameObject.name + " moveto " + data.target);
                isMoveTo = true;
                this.target = data.target;
            };
        }

        /// <summary>
        /// 初始化鼠标事件触发器
        /// </summary>
        public void InitTrigger()
        {
            // 将自身方法注册为UnityAction
            UnityAction<BaseEventData> drag = new UnityAction<BaseEventData>(MyOnMouseDrag);
            // 创建对应事件触发器
            EventTrigger.Entry myDrag = new EventTrigger.Entry();
            myDrag.eventID = EventTriggerType.Drag;
            myDrag.callback.AddListener(drag);

            UnityAction<BaseEventData> afterDrag = new UnityAction<BaseEventData>(MyOnMouseAfterDrag);
            EventTrigger.Entry myAfterDrag = new EventTrigger.Entry();
            myAfterDrag.eventID = EventTriggerType.PointerUp;
            myAfterDrag.callback.AddListener(afterDrag);

            UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(MyOnMouseDown);
            EventTrigger.Entry myBeginDrag = new EventTrigger.Entry();
            myBeginDrag.eventID = EventTriggerType.BeginDrag;
            myBeginDrag.callback.AddListener(click);

            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            trigger.triggers.Add(myDrag);
            trigger.triggers.Add(myAfterDrag);
            trigger.triggers.Add(myBeginDrag);
        }

        /// <summary>
        /// 鼠标拖动事件触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDrag(BaseEventData data)
        {

            // TODO：优化实现鼠标卡牌相对位置拖拽
            Vector3 mousePosition = new Vector3(Input.mousePosition.x - 910, Input.mousePosition.y, 0);

            transform.localPosition = mousePosition;

        }

        /// <summary>
        /// 鼠标拖动事件松开触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseAfterDrag(BaseEventData data)
        {
            // 若未进入召唤流程，则实现返回手牌动画
            if (SummonManager.Instance.waitingBucket == null)
            {
                MoveToData moveMessage = new MoveToData(gameObject, target);
                Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, moveMessage);
            }
            // 完成召唤确认
            else
            {
                Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, SummonManager.Instance.waitingBucket);

            }

        }

        /// <summary>
        /// 鼠标点击事件触发方法
        /// </summary>
        /// <param name="data"></param>
        void MyOnMouseDown(BaseEventData data)
        {
            // 实现召唤请求
            Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonRequest, gameObject);

        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch (areaCode)
            {
                case MessageArea.Summon:
                    switch (eventCode)
                    {
                        case MessageEvent.SummonEvent.SummonRequest:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonRequest, message as GameObject);
                            break;
                        case MessageEvent.SummonEvent.SummonConfirm:
                            MessageManager.Instance.Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, message as GameObject);
                            break;
                    }
                    break;
                case MessageArea.Card:
                    switch (eventCode)
                    {
                        case MessageEvent.CardEvent.MoveTo:
                            MessageManager.Instance.Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, message as MoveToData);
                            break;
                    }
                    break;
            }
        }


        public void Subscribe()
        {
            // 注册监听事件（订阅MoveTo类型消息）
            MessageManager.Instance.GetManager(MessageArea.Card).Subscribe<MoveToData>(MessageEvent.CardEvent.MoveTo, MoveTo);
        }

        public void RemoveSubscribe()
        {
            // TODO：研究在析构时解除订阅
            MessageManager.Instance.GetManager(MessageArea.Card).RemoveListener<MoveToData>(MessageEvent.CardEvent.MoveTo, MoveTo);
        }
    }
}