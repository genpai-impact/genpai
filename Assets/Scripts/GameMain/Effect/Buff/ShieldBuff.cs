
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

            buffName = BuffEnum.Shield;
            buffType = BuffType.DamageReduceBuff;

            storey = _storey;
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            target.buffAttachment.Remove(this);
        }

        public int GetIncrease()
        {
            return storey;
        }

        public void IncreaseBuff(int storeys = 0)
        {
            storey += storey;
        }

        public override int TakeDamage(int damage)
        {
            int surplusDamage = System.Math.Max(0, damage - storey);
            storey = System.Math.Max(0, storey - damage);

            return surplusDamage;
        }
    }
}
