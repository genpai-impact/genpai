
namespace Genpai
{
    /// <summary>
    /// 元素攻击
    /// </summary>
    public class ElementDamage
    {
        /// <summary>
        /// 造成的伤害
        /// </summary>
        public float Damage
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
    }
}
