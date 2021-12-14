namespace Genpai
{
    /// <summary>
    /// 伤害作用
    /// </summary>
    public class DamageEffect : BaseEffect
    {
        /// <summary>
        /// 造成的伤害
        /// </summary>
        public float DamageRate
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
        public DamageEffect(){
            // TODO: 创建具体伤害对象
        }

        public override void OnEffect(BaseUnit target){
            
        }
    }
}