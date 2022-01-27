using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class UnitEntity : MonoBehaviour, IDamageable, IMessageReceiveHandler
    {
        public GenpaiPlayer owner;  // ��λ������
        public bool actionState;    // ��λ�ж�״̬

        /// <summary>
        /// �ڵ�λʵ�崴��ʱ��ֵ��λ����
        /// </summary>
        public Unit unit;

        public LinkedList<Element> elementAttachment; // Ԫ�ظ����б����дд

        /// <summary>
        /// ����Ԫ��
        /// </summary>
        public Element ElementAttachment
        {
            get
            {
                if (unit.selfElement == ElementEnum.None)
                {
                    return elementAttachment.Last.Value;
                }
                else
                {
                    return new Element(unit.selfElement);
                }
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public int ATK
        {
            get
            {
                // ��ȡ����Buff
                return unit.baseATK;
            }
        }

        /// <summary>
        /// ����Ԫ��
        /// </summary>
        public ElementEnum ATKElement
        {
            get
            {
                // ��ȡ��ħBuff
                return unit.baseATKElement;
            }
        }


        /// <summary>
        /// ��ȡ��λ��ɵ��˺��ṹ��
        /// </summary>
        /// <returns>��λ����˺��ṹ</returns>
        public DamageStruct GetDamage()
        {
            return new DamageStruct(ATK, ATKElement);
        }

        /// <summary>
        /// �Ƿ�Զ�̹�����λ
        /// </summary>
        /// <returns></returns>
        public bool IsRemote()
        {
            // ���ҹ������� or Buff
            return false;
        }

        /// <summary>
        /// �����ڻغϿ�ʼʱ�����ж�״̬
        /// </summary>
        public void FreshActionState(bool _none)
        {
            actionState = true; //����
        }

        /// <summary>
        /// ����ʱ��ս������������
        /// (���������������֧�ֶ�ι��������ʵ��)
        /// </summary>
        public void BeActed()
        {
            actionState = false;
        }

        public void Subscribe()
        {
            // ���ĻغϿ�ʼ�¼���ˢ���ж�״̬��
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
        }


        void Awake()
        {
            Subscribe();
        }

    }
}