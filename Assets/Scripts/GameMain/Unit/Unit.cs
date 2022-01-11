using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 单位基类
    /// 符合显示特性及其他单位基础特性
    /// </summary>
    public class Unit : IMessageReceiveHandler
    {
        public PlayerID owner;
        public bool[] actionState;  // 单位行动状态（回合开始时更新）

        public int unitID;
        public string unitName;

        protected int HPMax;    // 血量上限
        protected int baseATK;  // 基准攻击
        protected readonly ElementEnum baseATKElement;    // 攻击元素
        protected readonly ElementEnum selfElement;        // 自身元素


        public List<BaseBuff> buffList; // Buff列表

        /// <summary>
        /// 生命值
        /// </summary>
        public int HP;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int ATK
        {
            get
            {
                // 获取攻击力修饰buff
                return baseATK;
            }
        }

        /// <summary>
        /// 攻击元素
        /// </summary>
        public ElementEnum ATKElement
        {
            get
            {
                if (this.baseATKElement is ElementEnum.None)
                {
                    // 获取附魔Buff
                    return this.baseATKElement;
                }
                else
                {
                    return this.baseATKElement;
                }
            }
        }

        protected Stack<Element> eleAttachment; // 元素附着列表（随便写写

        /// <summary>
        /// 自身元素
        /// </summary>
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
        /// 实现受伤及办白事
        /// </summary>
        public void TakeDamage()
        {

        }


        public void Execute(int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        // 订阅回合事件，重置行动
        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }
    }
}