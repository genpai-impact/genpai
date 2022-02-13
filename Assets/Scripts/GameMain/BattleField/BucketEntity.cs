using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 战场格子组件
    /// 用于联系战场与单位，以及提供单位所处位置特性
    /// </summary>
    public class BucketEntity : MonoBehaviour
    {
        public int serial;          // 格子序号
        public BattleSite ownerSite;
        public GenpaiPlayer owner
        {
            get
            {
                return GameContext.Instance.GetPlayerBySite(ownerSite);
            }
        }
        public bool tauntBucket;    // 嘲讽格子
        public bool charaBucket;    // 角色格子

        public UnitEntity unitCarry;

        private void Awake()
        {
            BucketUIController bucketSet = GetComponent<BucketUIController>();

            ownerSite = bucketSet.ownerSite;
            serial = bucketSet.serial;

            this.tauntBucket = serial == 1 | serial == 2 | serial == 8 | serial == 9; // 1、2号格子为嘲讽位
            this.charaBucket = serial == 5 | serial == 12;               // 5号格子为角色位
            this.unitCarry = null;
        }


        /// <summary>
        /// 绑定单位
        /// </summary>
        /// <param name="_unit">待绑定单位</param>
        public void BindUnit(UnitEntity _unit)
        {
            unitCarry = _unit;
        }


    }
}
