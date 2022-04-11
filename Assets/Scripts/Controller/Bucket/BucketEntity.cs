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

        public UnitEntity unitCarry;

        private void Awake()
        {
            BucketUIController bucket = GetComponent<BucketUIController>();

            ownerSite = bucket.ownerSite;
            serial = bucket.serial;

            unitCarry = null;
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
