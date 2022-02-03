﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{

    /// <summary>
    /// 元素Buff类
    /// 元素仅在附着时以Buff形式进行结算（攻击元素呈枚举）
    /// </summary>
    public class Element : BaseBuff
    {

        /// <summary>
        /// 元素类型
        /// </summary>
        public ElementEnum ElementType
        {
            set => ElementType = value;
            get
            {
                if (ElementLock)
                {
                    return ElementEnum.None;
                }
                else return ElementType;
            }
        }

        /// <summary>
        /// 元素锁，锁了之后认为不存在元素
        /// </summary>
        public bool ElementLock
        {
            set; get;
        }

        /// <summary>
        /// 构造元素Buff
        /// </summary>
        /// <param name="element">传入元素枚举（通常为受击时）</param>
        public Element(ElementEnum element)
        {
            ElementType = element;
            ElementLock = false;
        }

        /// <summary>
        /// 进行元素反应
        /// </summary>
        /// <param name="element">后手元素</param>
        public ElementReactionEnum ElementReaction(ElementEnum element)
        {
            ElementLock = true;

            // 返回反应类型予计算器处理
            return ElementReactionEnum.Burning;

        }



    }
}

