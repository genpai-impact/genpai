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
    public class BucketPlayerController : BaseClickHandle, IMessageSendHandler
    {

        public bool summoning = false;

        public void FuckingSummonCombo()
        {
            OnMouseEnter();
            OnMouseDown();
        }
        
        void OnMouseDown()
        {
            // Debug.Log("mouse Down");
            GenpaiMouseDown();
        }

        protected override void DoGenpaiMouseDown()
        {
            if (SummonManager.Instance.summonWaiting)
            {
                SummonManager.Instance.SummonConfirm();
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