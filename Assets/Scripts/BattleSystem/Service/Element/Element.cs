﻿namespace BattleSystem.Service.Element
{

    /// <summary>
    /// 元素Buff类
    /// 元素仅在附着时以Buff形式进行结算（攻击元素呈枚举）
    /// </summary>
    public class Element
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
            private set; get;
        }

        private bool ElementLockTemp
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
        /// 执行元素反应
        /// </summary>
        /// <param name="element">后手元素</param>
        /// <param name="simulator">是否为模拟</param>
        /// <returns>元素反应类型</returns>
        public ElementReactionEnum ElementReaction(ElementEnum element, bool simulator = false)
        {
            if (ElementLock)
            {
                return ElementReactionEnum.None;
            }

            ElementLockTemp = !simulator;
            // 返回反应类型予计算器处理
            switch ((int)ElementType | (int)element)
            {
                case 0x03: return ElementReactionEnum.Vaporise;
                case 0x05: return ElementReactionEnum.Melt;
                case 0x06: return ElementReactionEnum.Freeze;
                case 0x09: return ElementReactionEnum.Overload;
                case 0x0A: return ElementReactionEnum.ElectroCharge;
                case 0x0C: return ElementReactionEnum.Superconduct;
                case 0x11:
                case 0x12:
                case 0x14:
                case 0x18:
                    return element == ElementEnum.Anemo ? ElementReactionEnum.Swirl : ElementReactionEnum.None;
                case 0x21:
                case 0x22:
                case 0x24:
                case 0x28:
                    return element == ElementEnum.Geo ? ElementReactionEnum.Crystallise : ElementReactionEnum.None;
                default:
                    ElementLockTemp = false;
                    return ElementReactionEnum.None;
            }

        }

        public void FreshLock()
        {
            ElementLock = ElementLockTemp;
        }



    }
}
