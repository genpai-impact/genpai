
namespace Genpai
{
    /// <summary>
    /// 单位卡，可以是角色，也可以是怪物
    /// </summary>
    public class UnitCard : BaseCard
    {
        public int UnitID{
            get;set;
        }

        public override CardInfo GetDesc()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnUse(ITargetable target)
        {
            if (target is IUnitCarryTargetable)
            {
                // 作战单位对象在施法时创建
                // 忘了是否可以通过单位基类直接构造角色子类
                BaseUnit Unit = new BaseUnit(UnitID);
                ((IUnitCarryTargetable)target).SetUnit(Unit);
            }
        }
    }
}
