using Utils.Messager;

namespace BattleSystem.Service.Process
{
    /// <summary>
    /// 游戏中的某个流程
    /// </summary>
    public interface IProcess : IMessageSendHandler
    {
        /// <summary>
        /// 执行该流程
        /// </summary>
        public void Run();

        /// <summary>
        /// 获取该流程的名字
        /// </summary>
        public string GetName();
    }
}