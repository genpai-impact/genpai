using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Bucket : IMessageReceiveHandler
    {
        public int serial;          // �������
        public PlayerID owner;      // �������
        public bool tauntBucket;    // �������
        public bool charaBucket;    // ��ɫ����

        public Unit unitCarry;
        public GameObject bucketObj;

        public Bucket(PlayerID _owner, int _serial)
        {
            this.owner = _owner;
            this.serial = _serial;
            this.tauntBucket = (serial == 1 | serial == 2);
            this.charaBucket = (serial == 5);
            this.unitCarry = null;
            // Subscribe();
        }

        public void BindUnit(Unit _unit)
        {
            unitCarry = _unit;
        }

        public void Execute(int eventCode, object message)
        {
            // ����ٻ������������ٻ�ȷ��
            if (eventCode == 0)
            {
                BindUnit(message as Unit);
            }
        }

        public void Subscribe()
        {
            // ���ٻ�������׷�Ӷ���
        }
    }
}
