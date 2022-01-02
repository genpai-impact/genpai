namespace Genpai
{
    /// <summary>
    /// 游戏中的卡
    /// </summary>
    public abstract class BaseCard
    {
        /// <summary>
        /// 使用这张卡
        /// </summary>
        public abstract void Use(BattlegroundBucket target);
        /// <summary>
        /// 获取该卡的介绍
        /// </summary>
        public abstract CardInfo GetDesc();
        /// <summary>
        /// 判断该卡是否可以使用
        /// </summary>
        public abstract bool IsCanUse();
    }
}