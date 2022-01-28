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
    public class CardAniController : MonoBehaviour, IMessageReceiveHandler
    {

        public float smooth = 2;    //平滑移动系数
        public bool isMoveTo = false;      //移动控制器
        public Vector3 target;     //移动目标位置


        // Use this for initialization
        private void Awake()
        {
            Subscribe();
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