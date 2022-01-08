using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// ����UI��Ϊ
    /// </summary>
    public class BucketDisplay : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {

        // UI����
        public GameObject summonHighLight;
        public GameObject attackHighLight;

        // ��������
        public PlayerID owner;      // �������
        public int serial;          // ������ţ�����������Ϣ��

        public Bucket bucket;       // ��Ӧ����

        private void Awake()
        {
            bucket = new Bucket(owner, serial);
        }
        // ��λ��ȡ
        // public GameObject unitCarry;

        public void SetSummon()
        {
            summonHighLight.SetActive(true);
        }

        public void SetAttack()
        {
            attackHighLight.SetActive(true);
        }

        public void SetIdle()
        {
            summonHighLight.SetActive(false);
            attackHighLight.SetActive(false);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (SummonManager.Instance.summonWaiting)
            {
                if (owner == SummonManager.Instance.waitingPlayer && bucket.unitCarry == null && !bucket.charaBucket)
                {
                    // ���ٻ�������������Ϣ�����ӱ������ִ���ٻ�ȷ��
                    Dispatch(MessageArea.Behavior, 0, this);
                }
            }
        }


        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }


        public void Execute(int eventCode, object message)
        {
            // ����ս��������UI��Ϣ���ݲ�ͬ��Ϣ�Ը��ӽ��и���
        }


        public void Subscribe()
        {
            // ����UI������
        }
    }
}