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
            set;
            get;
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
            ElementReactionEnum reaction = ElementReactionEnum.None;

            // 如果元素相同不进行反应
            if (ElementType == element)
            {
                return reaction;
            }

            // 确定进行反应则先上锁
            ElementLock = true;

            switch (ElementType)
            {
                // 火元素反应
                case ElementEnum.Pyro:
                    switch (element)
                    {
                        case ElementEnum.Pyro:
                            break;
                        case ElementEnum.Hydro:
                            reaction = ElementReactionEnum.Vaporise;
                            break;
                        case ElementEnum.Cryo:
                            reaction = ElementReactionEnum.Melt;
                            break;
                        case ElementEnum.Electro:
                            reaction = ElementReactionEnum.Overload;
                            break;
                        case ElementEnum.Anemo:
                            reaction = ElementReactionEnum.Swirl;
                            break;
                        case ElementEnum.Geo:
                            reaction = ElementReactionEnum.Crystallise;
                            break;
                    }
                    break;
                // 水元素反应
                case ElementEnum.Hydro:
                    switch (element)
                    {
                        case ElementEnum.Pyro:
                            reaction = ElementReactionEnum.Vaporise;
                            break;
                        case ElementEnum.Hydro:
                            break;
                        case ElementEnum.Cryo:
                            reaction = ElementReactionEnum.Freeze;
                            break;
                        case ElementEnum.Electro:
                            reaction = ElementReactionEnum.ElectroCharge;
                            break;
                        case ElementEnum.Anemo:
                            reaction = ElementReactionEnum.Swirl;
                            break;
                        case ElementEnum.Geo:
                            reaction = ElementReactionEnum.Crystallise;
                            break;
                    }
                    break;
                // 冰元素反应
                case ElementEnum.Cryo:
                    switch (element)
                    {
                        case ElementEnum.Pyro:
                            reaction = ElementReactionEnum.Melt;
                            break;
                        case ElementEnum.Hydro:
                            reaction = ElementReactionEnum.Freeze;
                            break;
                        case ElementEnum.Cryo:
                            break;
                        case ElementEnum.Electro:
                            reaction = ElementReactionEnum.Superconduct;
                            break;
                        case ElementEnum.Anemo:
                            reaction = ElementReactionEnum.Swirl;
                            break;
                        case ElementEnum.Geo:
                            reaction = ElementReactionEnum.Crystallise;
                            break;
                    }
                    break;
                // 雷元素反应
                case ElementEnum.Electro:
                    switch (element)
                    {
                        case ElementEnum.Pyro:
                            reaction = ElementReactionEnum.Overload;
                            break;
                        case ElementEnum.Hydro:
                            reaction = ElementReactionEnum.ElectroCharge;
                            break;
                        case ElementEnum.Cryo:
                            reaction = ElementReactionEnum.Superconduct;
                            break;
                        case ElementEnum.Electro:
                            break;
                        case ElementEnum.Anemo:
                            reaction = ElementReactionEnum.Swirl;
                            break;
                        case ElementEnum.Geo:
                            reaction = ElementReactionEnum.Crystallise;
                            break;
                    }
                    break;

            }

            // 返回反应类型予计算器处理
            return reaction;

        }



    }
}

