namespace Genpai
{
    /// <summary>
    /// 游戏中的卡
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// 使用这张卡
        /// </summary>
        public void Use(ISkillTargetable target);
        /// <summary>
        /// 获取该卡的名字
        /// </summary>
        public string GetName();
        /// <summary>
        /// 获取该卡的介绍
        /// </summary>
        public string GetDesc();
        /// <summary>
        /// 获取该卡的类型
        /// </summary>
        public string GetType();
    }
}