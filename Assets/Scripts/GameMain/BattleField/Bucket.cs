using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Bucket : IMessageReceiveHandler
    {
        public int serial;          // 格子序号
        public PlayerID owner;      // 所属玩家
        public bool tauntBucket;    // 嘲讽格子
        public bool charaBucket;    // 角色格子

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
            // 如果召唤管理器返回召唤确认
            if (eventCode == 0)
            {
                BindUnit(message as Unit);
            }
        }

        public void Subscribe()
        {
            // 向召唤管理器追加订阅
        }
    }
}
