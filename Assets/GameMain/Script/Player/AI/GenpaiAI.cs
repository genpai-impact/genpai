
namespace Genpai
{
    /// <summary>
    /// 游戏内的AI
    /// </summary>
    public abstract class BaseGenpaiAI : GenpaiController
    {
        /// <summary>
        /// 启动AI
        /// </summary>
        public override void StartRound()
        {
            AILogic();
        }

        /// <summary>
        /// AI执行的逻辑
        /// </summary>
        protected abstract void AILogic();

    }
}
