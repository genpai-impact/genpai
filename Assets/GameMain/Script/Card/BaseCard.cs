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
        public void Use(ITargetable target)
        {
            BeforeUse(target);
            OnUse(target);
            AfterUse(target);
        }
        /// <summary>
        /// 获取该卡的介绍
        /// </summary>
        public abstract CardInfo GetDesc();
        /// <summary>
        /// 使用前执行的操作
        /// </summary>
        /// <param name="target"></param>
        public void BeforeUse(ITargetable target)
        {
        }
        /// <summary>
        /// 使用后执行的操作
        /// </summary>
        /// <param name="target"></param>
        public void AfterUse(ITargetable target)
        {
            // todo 移动到墓地。
        }
        /// <summary>
        /// 使用时进行的操作
        /// </summary>
        /// <param name="target"></param>
        protected abstract void OnUse(ITargetable target);
    }
}