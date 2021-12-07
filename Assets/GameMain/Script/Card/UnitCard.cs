
namespace Genpai
{
    /// <summary>
    /// 单位卡，可以是角色，也可以是怪物
    /// </summary>
    public class UnitCard : BaseCard
    {
        public IUnit Unit
        {
            get; set;
        }

        public override CardInfo GetDesc()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUse(ITargetable target)
        {
            if (target is IUnitCarryTargetable)
            {
                ((IUnitCarryTargetable)target).SetUnit(Unit);
            }
        }
    }
}
