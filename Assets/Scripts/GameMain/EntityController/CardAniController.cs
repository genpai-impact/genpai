using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{

    /// <summary>
    /// 卡牌动画管理器
    /// 主要管理卡牌的移动动画
    /// </summary>
    public class CardAniController : MonoBehaviour, IMessageReceiveHandler
    {

        

        /// <summary>
        /// TODO：由HandCardManager动态分配位置
        /// </summary>
        public Vector3 targetPosition;      //移动目标位置
        public Transform _transform;
        private int times;

        // Use this for initialization
        private void Awake()
        {
            times = 0;
            Subscribe();
            _transform = transform;
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 origin = transform.localPosition;
            float x = 0.93f;
            if (times++<80) {
                
                Vector3 temp = Vector3.Lerp(origin, targetPosition, -x+1);
                _transform.localPosition = temp;
                _transform.localPosition += new Vector3(1,0,0) ;
                x *= 0.93f;
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
                times = 0;
                targetPosition = data.target;
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