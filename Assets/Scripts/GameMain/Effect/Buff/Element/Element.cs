using System.Collections;
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
            set; get;
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
        /// <param name="element">传入枚举元素（通常为受击时）</param>
        public Element(ElementEnum element)
        {
            this.ElementType = element;
        }

        /// <summary>
        /// 进行元素反应
        /// </summary>
        /// <param name="element"></param>
        public void ElementReaction(Element element)
        {
            // todo 仔细观察元素反应文档，想想怎么设计元素反应
        }



    }
}

