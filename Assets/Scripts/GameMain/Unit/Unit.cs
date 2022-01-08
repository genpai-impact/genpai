using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Unit : IMessageReceiveHandler
    {
        public PlayerID owner;
        public bool[] actionState;

        public int unitID;
        public string unitName;

        protected int HPMax;    // 血量上限
        protected int baseATK;  // 基准攻击
        protected readonly ElementEnum baseATKElement;    //攻击元素
        protected readonly ElementEnum selfElement;        //自身元素

        public int HP;
        // 获取攻击
        public int ATK
        {
            get
            {
                // 获取攻击力修饰buff
                return baseATK;
            }
        }
        // 获取攻击元素
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
        // 返回当前元素Buff
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
        /// 传入伤害类
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