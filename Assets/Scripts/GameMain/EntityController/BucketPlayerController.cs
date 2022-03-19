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
    public class BucketPlayerController : MonoBehaviour, IMessageSendHandler
    {

        public bool summoning = false;

        void OnMouseDown()
        {
            if (SummonManager.Instance.summonWaiting)
            {
                Dispatch(MessageArea.Summon, MessageEvent.SummonEvent.SummonConfirm, gameObject);
            }
        }

        /// <summary>
        /// 鼠标移入时更新等待召唤格子
        /// </summary>
        void OnMouseEnter()
        {
            if (summoning)
            {
                SummonManager.Instance.waitingBucket = gameObject;
            }
        }

        /// <summary>
        /// 鼠标移出时更新等待召唤格子
        /// </summary>
        void OnMouseExit()
        {
            SummonManager.Instance.waitingBucket = null;
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            switch (areaCode)
            {
                case MessageArea.Summon:
                    MessageManager.Instance.Dispatch(areaCode, eventCode, message as GameObject);
                    break;
            }
        }
    }
}