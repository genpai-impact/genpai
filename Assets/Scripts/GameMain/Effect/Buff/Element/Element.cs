using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{

    /// <summary>
    /// 元素Buff类
    /// 元素仅在附着时以Buff形式进行结算
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

