
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
                // 角色构造和标准单位构造如何区分
                BaseUnit Unit = new BaseUnit(UnitID);
                ((IUnitCarryTargetable)target).SetUnit(ref Unit);
            }
        }
    }
}
