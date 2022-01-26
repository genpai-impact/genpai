using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class UnitEntity : MonoBehaviour, IDamageable
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}