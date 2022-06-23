using Utils;

namespace GameSystem.LevelSystem
{
    public class LevelInfoDontDestroy : MonoSingleton<LevelInfoDontDestroy>
    {
        void Awake()
        {
            // 注册无法在切换时销毁，用于保存关卡信息
            DontDestroyOnLoad(this.gameObject);
        }
        
        # region 设计考量
        // LevelInfoDontDestroy （暂定） 主要用于寄存当前关卡信息
        // 当玩家进入BattleScene时，主文件将会读取
        # endregion

    }
}
