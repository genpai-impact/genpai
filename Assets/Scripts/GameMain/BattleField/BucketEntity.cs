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
    public class BucketEntity : MonoBehaviour
    {
        public int serial;          // 格子序号
        public GenpaiPlayer owner;      // 所属玩家
        public bool tauntBucket;    // 嘲讽格子
        public bool charaBucket;    // 角色格子

        public Unit unitCarry;

        private void Awake()
        {
            BucketUIController bucketSet = GetComponent<BucketUIController>();

            owner = GameContext.Instance.GetPlayerBySite(bucketSet.ownerSite);
            serial = bucketSet.serial;

            this.tauntBucket = serial == 1 | serial == 2 | serial == 8 | serial == 9; // 1、2号格子为嘲讽位
            this.charaBucket = serial == 5 | serial == 12;               // 5号格子为角色位
            this.unitCarry = null;
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
