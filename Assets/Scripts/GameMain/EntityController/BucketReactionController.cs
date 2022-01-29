using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// ���ӽ����ű�
    /// ��Ҫ�����ٻ�����
    /// </summary>
    public class BucketReactionController : MonoBehaviour, IMessageSendHandler
    {

        public bool summoning = false;

        /// <summary>
        /// �������ʱ���µȴ��ٻ�����
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
        /// ����Ƴ�ʱ���µȴ��ٻ�����
        /// </summary>
        void OnMouseExit()
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            SummonManager.Instance.waitingBucket = null;
        }

        /// <summary>
        /// �������ʵ���ٻ�ȷ��
        /// TODO��ͬCardControlerʵ��
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