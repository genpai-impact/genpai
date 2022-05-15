
using Messager;

namespace Genpai
{

    /// <summary>
    /// 持续伤害类Buff
    /// </summary>
    public abstract class BaseDamageOverTimeBuff : BaseBuff, IDamageable, IMessageReceiveHandler
    {
        public int DamageValue;
        public ElementEnum DamageElement;

        // Buff层数
        public int Storey;

        public DamageStruct GetDamage()
        {
            return new DamageStruct(DamageValue * Storey, DamageElement);
        }

        /// <summary>
        /// 订阅Dot实现时间
        /// </summary>
        public virtual void Subscribe()
        {
        }
    }
}
