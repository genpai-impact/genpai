using Utils;

namespace GameSystem
{
    public class LevelInfoDontDestroy : MonoSingleton<LevelInfoDontDestroy>
    {
        void Awake()
        {
            // 注册无法在切换时销毁，用于保存关卡信息
            DontDestroyOnLoad(this.gameObject);
        }
    
    }
}
