﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 单位基类
    /// 符合显示特性及其他单位基础特性
    /// </summary>
    public class Unit
    {

        public readonly int unitID;
        public readonly string unitName;

        public readonly int HPMax;    // 血量上限
        public int baseATK;  // 基准攻击
        public readonly ElementEnum baseATKElement;  // 攻击元素
        public readonly ElementEnum selfElement;     // 自身元素

        /// <summary>
        /// 当前生命
        /// </summary>
        public int HP;

        public Unit(UnitCard unitCard)
        {
            this.unitID = unitCard.cardID;
            this.unitName = unitCard.cardName;

            this.HPMax = unitCard.hp;
            this.baseATK = unitCard.atk;
            this.baseATKElement = unitCard.atkElement;
            this.selfElement = unitCard.selfElement;

            this.HP = unitCard.hp;
        }

    }
}