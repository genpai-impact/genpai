
namespace Genpai
{
    /// <summary>
    /// 战场上的格子
    /// </summary>
    public class BattlegroundBucket : IUnitCarryTargetable
    {
        /// <summary>
        /// 这个格子上的作战单位
        /// </summary>
        public BaseUnit Unit;

        public void SetUnit(BaseUnit unit)
        {
            if (unit != null)
            {
                return;
            }
            Unit = unit;
        }
    }
}
