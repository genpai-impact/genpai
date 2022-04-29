namespace Genpai
{
    /// <summary>
    /// 受击承伤类Buff
    /// </summary>
    public abstract class BaseDamageReduceBuff : BaseBuff
    {
        // Buff层数
        public int storey;

        /// <summary>
        /// 伤害从此过
        /// </summary>
        /// <param name="damage">进入伤害</param>
        /// <returns>出去伤害</returns>
        public virtual int TakeDamage(int damage)
        {
            return damage;
        }
    }
}
