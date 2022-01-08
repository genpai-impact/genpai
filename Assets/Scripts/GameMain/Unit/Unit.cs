using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Unit : IMessageReceiveHandler
    {
        public PlayerID owner;
        public bool[] actionState;

        public int unitID;
        public string unitName;

        protected int HPMax;    // Ѫ������
        protected int baseATK;  // ��׼����
        protected readonly ElementEnum baseATKElement;    //����Ԫ��
        protected readonly ElementEnum selfElement;        //����Ԫ��

        public int HP;
        // ��ȡ����
        public int ATK
        {
            get
            {
                // ��ȡ����������buff
                return baseATK;
            }
        }
        // ��ȡ����Ԫ��
        public ElementEnum ATKElement
        {
            get
            {
                if (this.baseATKElement is ElementEnum.None)
                {
                    // ��ȡ��ħBuff
                    return this.baseATKElement;
                }
                else
                {
                    return this.baseATKElement;
                }
            }
        }

        protected Stack<Element> eleAttachment; // Ԫ�ظ����б����дд
        // ���ص�ǰԪ��Buff
        public Element EleAttachment
        {
            get
            {
                if (selfElement == ElementEnum.None)
                {
                    return eleAttachment.Pop();
                }
                else
                {
                    return new Element(selfElement);
                }
            }
        }


        public Unit(UnitCard unitCard, PlayerID _owner)
        {
            this.unitID = unitCard.cardID;
            this.unitName = unitCard.cardName;
            this.HP = unitCard.hp;
            this.HPMax = unitCard.hp;
            this.baseATK = unitCard.atk;
            this.baseATKElement = unitCard.atkElement;
            this.selfElement = unitCard.selfElement;
            this.owner = _owner;
        }

        /// <summary>
        /// �����˺���
        /// </summary>
        public void TakeDamage()
        {

        }


        public void Execute(int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        // ���Ļغ��¼��������ж�
        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }
    }
}