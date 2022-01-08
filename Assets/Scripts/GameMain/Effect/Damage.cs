using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Damage : MonoBehaviour, IMessageSendHandler
    {
        /// <summary>
        /// �����˺�������ִ�е���һ���˺�
        /// </summary>
        public Damage Next
        {
            get; set;
        }
        /// <summary>
        /// �˺���Դ
        /// </summary>
        public GenpaiPlayer Resource
        {
            get; set;
        }
        /// <summary>
        /// �˺�Ŀ��
        /// </summary>
        public Unit Target
        {
            get; set;
        }
        /// <summary>
        /// ��ɵ��˺�
        /// </summary>
        public float DamageValue
        {
            get; set;
        }
        /// <summary>
        /// ���ι�����Ԫ������
        /// </summary>
        public Element Element
        {
            get; set;
        }

        /// <summary>
        /// ���б�������һ���˺�
        /// </summary>
        /// <param name="newDamage"></param>
        public void AddDamage(Damage newDamage)
        {
            if (Next == null)
            {
                Next = newDamage;
            }
            Damage temp = Next;
            for (; temp.Next != null;)
            {
                temp = temp.Next;
            }
            temp.Next = newDamage;
        }
        /// <summary>
        /// ����˺�
        /// </summary>
        public void DoDamage()
        {
            Dispatch(MessageArea.Damage, 0, 0);
            if (Next != null)
            {
                Next.DoDamage();
            }
        }


        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }


    }
}