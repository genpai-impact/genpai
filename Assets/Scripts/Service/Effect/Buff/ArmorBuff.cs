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

            buffName = BuffEnum.Armor;
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
            storey += storeys;
        }

        public override int TakeDamage(int damage)
        {
            return System.Math.Max(0, damage - storey);
        }
    }
}
