using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 抽象卡牌容器
    /// </summary>
    public abstract class CardContainer
    {
        /// <summary>
        /// 卡牌容器
        /// </summary>
        public List<BaseCard> cardContainer{
            get;
            set;
        }
    }
}