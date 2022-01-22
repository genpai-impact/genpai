﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 普通攻击、技能生成伤害类，伤害结算由EffectManager受理
    /// </summary>
    public class Damage : IMessageSendHandler
    {
        /// <summary>
        /// 本次伤害结束后，执行的下一个伤害
        /// </summary>
        public Damage Next
        {
            get; set;
        }
        /// <summary>
        /// 伤害来源
        /// </summary>
        public GenpaiPlayer Resource
        {
            get; set;
        }
        /// <summary>
        /// 伤害目标
        /// </summary>
        public Unit Target
        {
            get; set;
        }
        /// <summary>
        /// 造成的伤害
        /// </summary>
        public float DamageValue
        {
            get; set;
        }
        /// <summary>
        /// 本次攻击的元素属性
        /// </summary>
        public Element Element
        {
            get; set;
        }

        /// <summary>
        /// 向列表内新增一个伤害
        /// </summary>
        /// <param name="newDamage"></param>
        public void AddDamage(Damage newDamage)
        {
            if (Next == null)
            {
                Next = newDamage;
            }
            Damage temp = Next;
            for (; temp.Next != null;)
            {
                temp = temp.Next;
            }
            temp.Next = newDamage;
        }
        /// <summary>
        /// 造成伤害
        /// </summary>
        public void DoDamage()
        {
            // 通过将自身授予EffectManager进行统一结算
            // Dispatch(MessageArea.Effect, 0, this);
            if (Next != null)
            {
                Next.DoDamage();
            }
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }


    }
}