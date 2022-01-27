using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class UnitEntity : MonoBehaviour, IDamageable, IMessageReceiveHandler
    {
        public GenpaiPlayer owner;  // 单位所有者
        public bool actionState;    // 单位行动状态

        /// <summary>
        /// 在单位实体创建时赋值单位属性
        /// </summary>
        public Unit unit;

        public LinkedList<Element> elementAttachment; // 元素附着列表（随便写写

        /// <summary>
        /// 自身元素
        /// </summary>
        public Element ElementAttachment
        {
            get
            {
                if (unit.selfElement == ElementEnum.None)
                {
                    return elementAttachment.Last.Value;
                }
                else
                {
                    return new Element(unit.selfElement);
                }
            }
        }

        /// <summary>
        /// 攻击力
        /// </summary>
        public int ATK
        {
            get
            {
                // 获取攻击Buff
                return unit.baseATK;
            }
        }

        /// <summary>
        /// 攻击元素
        /// </summary>
        public ElementEnum ATKElement
        {
            get
            {
                // 获取附魔Buff
                return unit.baseATKElement;
            }
        }


        /// <summary>
        /// 获取单位造成的伤害结构体
        /// </summary>
        /// <returns>单位造成伤害结构</returns>
        public DamageStruct GetDamage()
        {
            return new DamageStruct(ATK, ATKElement);
        }

        /// <summary>
        /// 是否远程攻击单位
        /// </summary>
        /// <returns></returns>
        public bool IsRemote()
        {
            // 查找固有属性 or Buff
            return false;
        }

        /// <summary>
        /// 用于在回合开始时重置行动状态
        /// </summary>
        public void FreshActionState(bool _none)
        {
            actionState = true; //草率
        }

        /// <summary>
        /// 攻击时由战斗管理器调用
        /// (如果后续固有属性支持多次攻击则调整实现)
        /// </summary>
        public void BeActed()
        {
            actionState = false;
        }

        public void Subscribe()
        {
            // 订阅回合开始事件（刷新行动状态）
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
        }


        void Awake()
        {
            Subscribe();
        }

    }
}