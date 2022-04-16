
namespace Genpai
{
    /// <summary>
    /// csv读取魔法卡信息的结构
    /// </summary>
    public class SpellCardData
    {
        public int CardID;
        public string CardName;
        public ElementEnum ElementType;
        public int BaseNumericalValue;

        /// <summary>
        /// 魔法卡同元素增强时的数值
        /// <para>只有增强类型为数值增强的卡才填这一项；增强类型为其他的卡，填0</para>
        /// </summary>
        public int EnhanceNumericalValue;

        public string CardInfo;
        public string ClassName;
    }
}