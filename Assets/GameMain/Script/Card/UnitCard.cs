
namespace Genpai
{
    /// <summary>
    /// 单位卡，可以是角色，也可以是怪物
    /// </summary>
    public class UnitCard : BaseCard
    {
        public int UnitId
        {
            get; set;
        }

        public override CardInfo GetDesc()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsCanUse()
        {
            throw new System.NotImplementedException();
        }

        public override void Use(BattlegroundBucket target)
        {
            throw new System.NotImplementedException();
        }
    }
}
