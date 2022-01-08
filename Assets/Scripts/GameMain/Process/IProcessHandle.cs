
namespace Genpai
{
    /// <summary>
    /// 可随着流程进行操作的
    /// 待弃用，使用消息系统代替
    /// </summary>
    public interface IProcessHandle
    {
        public void OnGameStart();
        public void OnRoundStart();
        public void OnRound();
        public void OnRoundEnd();
        public void OnBossStart();
        public void OnGameEnd();
    }
}
