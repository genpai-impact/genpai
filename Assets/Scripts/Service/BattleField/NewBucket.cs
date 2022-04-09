using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class NewBucket
    {
        public int serial;

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

        public NewUnit unitCarry;

        public NewBucket(int _serial, BattleSite _ownerSite)
        {
            serial = _serial;
            ownerSite = _ownerSite;
            Init();
        }

        /// <summary>
        /// 初始化基本属性
        /// </summary>
        private void Init()
        {
            this.tauntBucket = serial == 1 | serial == 2 | serial == 8 | serial == 9; // 1、2号格子为嘲讽位
            this.charaBucket = serial == 5 | serial == 12;               // 5号格子为角色位
            this.unitCarry = null;
        }

        /// <summary>
        /// 绑定单位
        /// </summary>
        public void BindUnit(NewUnit _unit)
        {
            unitCarry = _unit;
        }

    }
}