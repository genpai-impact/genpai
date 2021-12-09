
namespace Genpai
{
    /// <summary>
    /// 游戏内的AI
    /// </summary>
    public abstract class BaseGenpaiAI
    {
        /// <summary>
        /// 启动AI
        /// </summary>
        public void Run()
        {
            AILogic();
            GameContext.processManager.EndRound();
        }

        /// <summary>
        /// AI执行的逻辑
        /// </summary>
        protected abstract void AILogic();

    }
}
