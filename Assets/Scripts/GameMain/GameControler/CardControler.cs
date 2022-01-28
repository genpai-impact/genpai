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
    public class CardControler : MonoBehaviour,IMessageHandler
    {

        public float smooth = 2;//平滑移动系数
        bool isMoveTo = false;//移动控制器
        private Vector3 target;//移动目标

        public void RemoveSubscribe() {
            Messager.MessageManager.Instance.GetManager(Messager.MessageArea.Card).RemoveListener<MoveToData>(Messager.MessageEvent.CardEvent.MoveTo, MoveTo);
        }
        // Use this for initialization
        private void Awake()
        {
            // 注册监听事件（订阅MoveTo类型消息）
            Messager.MessageManager.Instance.GetManager(Messager.MessageArea.Card).Subscribe<MoveToData>(Messager.MessageEvent.CardEvent.MoveTo, MoveTo);
            UnityAction<BaseEventData> drag = new UnityAction<BaseEventData>(MyOnMouseDrag);
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
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (isMoveTo)
            {
                Vector3 temp = Vector3.Lerp(this.transform.localPosition, target, Time.deltaTime * smooth);
                this.transform.localPosition = temp;

                //Debug.Log("moving");
                if (System.Math.Abs(transform.localPosition.x-target.x)<= 0.1)
                    isMoveTo = false;
            }
        }

        /// <summary>
        /// 触发监听事件
        /// </summary>
        /// <param name="data">监听事件传入消息</param>
        public void MoveTo(MoveToData data)
        {
            
            if (this.gameObject == data.gameObject)
            {
                Debug.LogWarning(gameObject.name+" moveto " + data.target);
                isMoveTo = true;
                this.target = data.target;
            };
        }


        void MyOnMouseAfterDrag(BaseEventData data)
        {
            MoveToData moveMessage = new MoveToData(gameObject,target);
            if (BattleFieldManager.Instance.waitingBucket == null)
            {
                MessageManager.Instance.Dispatch(MessageArea.Card, MessageEvent.CardEvent.MoveTo, moveMessage);
            }
            else {
                SummonData message = new SummonData(GameContext.Player1,gameObject, BattleFieldManager.Instance.waitingBucket);
                SummonManager.Instance.Dispatch(MessageArea.Summon, "Summon", message);
            }
           
            //Debug.LogWarning("Afterdraging");

            SummonManager.Instance.Dispatch(MessageArea.Summon, "SummonEnd", false);
        }

        void MyOnMouseDrag(BaseEventData data)
        {
            Debug.Log("draging");
            //获取到鼠标的位置(鼠标水平的输入和竖直的输入以及距离)
            Vector3 mousePosition = new Vector3(Input.mousePosition.x - 910, Input.mousePosition.y, 0);
            //Debug.Log(mousePosition);
            //物体的位置，屏幕坐标转换为世界坐标
            //Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //把鼠标位置传给物体
            transform.localPosition = mousePosition;
            //MessageManager.Instance.Dispatch<>();

            
        }

        void MyOnMouseDown(BaseEventData data)
        {
           

            SummonManager.Instance.Dispatch(MessageArea.Summon, "SummonRequest", this.gameObject);
        }

        void MyOnMouseUp(BaseEventData data)
        {
            Debug.Log("draging");
            //获取到鼠标的位置(鼠标水平的输入和竖直的输入以及距离)
            Vector3 mousePosition = new Vector3(Input.mousePosition.x - 910, Input.mousePosition.y, 0);
            //Debug.Log(mousePosition);
            //物体的位置，屏幕坐标转换为世界坐标
            //Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //把鼠标位置传给物体
            transform.localPosition = mousePosition;
            //MessageManager.Instance.Dispatch<>();

            SummonManager.Instance.Dispatch(MessageArea.Summon, "SummonRequest", this.gameObject);
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
    }
}