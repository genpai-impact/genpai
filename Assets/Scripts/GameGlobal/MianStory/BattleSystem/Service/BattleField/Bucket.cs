﻿using BattleSystem.Service.Common;
using BattleSystem.Service.Player;

namespace BattleSystem.Service.BattleField
{
    public class Bucket
    {
        public int serial;

        public BattleSite ownerSite;

        public GenpaiPlayer owner
        {
            get
            {
                return GameContext.GetPlayerBySite(ownerSite);
            }
        }

        public bool tauntBucket;    // 嘲讽格子
        public bool charaBucket;    // 角色格子

        public Unit.Unit unitCarry;

        public Bucket(int _serial)
        {
            serial = _serial;
            Init();
        }

        /// <summary>
        /// 初始化基本属性
        /// 后可能根据规则临时更改需求抽象设置函数
        /// </summary>
        private void Init()
        {
            this.ownerSite = serial == 0 ? BattleSite.Boss : (serial < 8 ? BattleSite.P1 : BattleSite.P2);
            this.tauntBucket = serial == 1 | serial == 2 | serial == 8 | serial == 9; // 1、2号格子为嘲讽位
            this.charaBucket = serial == 5 | serial == 12;               // 5号格子为角色位
            this.unitCarry = null;
        }

        /// <summary>
        /// 绑定单位（限BattleFieldManager调用）
        /// </summary>
        public void BindUnit(Unit.Unit _unit)
        {
            unitCarry = _unit;
            // Debug.Log(unitCarry.unitName + "Summoned on serial" + serial);
        }

    }
}