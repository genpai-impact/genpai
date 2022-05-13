
namespace Genpai
{
    /// <summary>
    /// 护盾Buff
    /// 受伤掉层
    /// </summary>
    public class ShieldBuff : BaseDamageReduceBuff, IBuffDeleteable, IBuffIncreasable
    {
        public ShieldBuff(int _storey)
        {

            BuffName = BuffEnum.Shield;
            BuffType = BuffType.DamageReduceBuff;

            Storey = _storey;
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            Target.BuffAttachment.Remove(this);
        }

        public int GetIncrease()
        {
            return Storey;
        }

        public void IncreaseBuff(int storeys = 0)
        {
            Storey += Storey;
        }

        public override int TakeDamage(int damage)
        {
            int surplusDamage = System.Math.Max(0, damage - Storey);
            Storey = System.Math.Max(0, Storey - damage);

            return surplusDamage;
        }
    }
}
