using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 战场格子类
    /// 用于联系战场与单位，以及提供单位所处位置特性
    /// </summary>
    public class Bucket
    {
        public int serial;          // 格子序号
        public PlayerSite owner;      // 所属玩家
        public bool tauntBucket;    // 嘲讽格子
        public bool charaBucket;    // 角色格子

        public Unit unitCarry;
        public GameObject bucketObj;

        /// <summary>
        /// 构造格子，通过设定格子所有者及格子序号实现
        /// </summary>
        /// <param name="_owner"></param>
        /// <param name="_serial"></param>
        public Bucket(PlayerSite _owner, int _serial)
        {
            this.owner = _owner;
            this.serial = _serial;
            this.tauntBucket = (serial == 1 | serial == 2); // 1、2号格子为嘲讽位
            this.charaBucket = (serial == 5);               // 5号格子为角色位
            this.unitCarry = null;                          // 默认无怪物
            // Subscribe();
        }

        /// <summary>
        /// 绑定单位
        /// </summary>
        /// <param name="_unit">待绑定单位</param>
        public void BindUnit(Unit _unit)
        {
            unitCarry = _unit;
        }


    }
}
