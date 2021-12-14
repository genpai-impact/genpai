namespace Genpai
{
    /// <summary>
    /// 恢复作用
    /// </summary>
    public class HealingEffect : BaseEffect
    {
        /// <summary>
        /// 造成的恢复
        /// </summary>
        public float HealingRate
        {
            get; set;
        }
        public HealingEffect(){
            // TODO: 创建具体恢复对象
        }

        public override void OnEffect(BaseUnit target){
            
        }
    }
}