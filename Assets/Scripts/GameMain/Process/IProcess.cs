namespace Genpai
{
    /// <summary>
    /// 游戏中的某个流程
    /// </summary>
    public interface IProcess
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