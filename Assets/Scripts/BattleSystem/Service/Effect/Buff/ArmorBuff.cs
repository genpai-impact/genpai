namespace Genpai
{
    /// <summary>
    /// 护甲Buff
    /// 受到伤害不掉层
    /// </summary>
    public class ArmorBuff : BaseDamageReduceBuff, IBuffDeleteable, IBuffIncreasable
    {
        public ArmorBuff(int _storey)
        {

            BuffName = BuffEnum.Armor;
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
            Storey += storeys;
        }

        public override int TakeDamage(int damage)
        {
            return System.Math.Max(0, damage - Storey);
        }
    }
}
