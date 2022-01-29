using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 格子交互脚本
    /// 主要用于召唤流程
    /// </summary>
    public class BucketReactionController : MonoBehaviour, IMessageSendHandler
    {

        public bool summoning = false;

        /// <summary>
        /// 鼠标移入时更新等待召唤格子
        /// </summary>
        void OnMouseEnter()
        {
            if (summoning)
            {
                transform.localScale = new Vector3(0.11f, 0.11f, 0.1f);

                SummonManager.Instance.waitingBucket = gameObject;
            }
        }

        /// <summary>
        /// 鼠标移出时更新等待召唤格子
        /// </summary>
        void OnMouseExit()
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            SummonManager.Instance.waitingBucket = null;
        }

        /// <summary>
        /// 点击格子实现召唤确认
        /// TODO：同CardControler实现
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (SummonManager.Instance.summonWaiting)
            {
                Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, SummonManager.Instance.waitingBucket);
            }
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }
    }
}