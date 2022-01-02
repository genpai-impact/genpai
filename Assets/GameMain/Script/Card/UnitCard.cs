
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

        protected override void OnUse(BattlegroundBucket target)
        {
            // todo 工厂模式来生成
        }
    }
}
