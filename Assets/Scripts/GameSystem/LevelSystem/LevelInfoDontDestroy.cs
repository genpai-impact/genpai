using cfg.level;
using DataScripts;
using UnityEngine.Serialization;
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
        // 当玩家进入BattleScene时，主进程文件将根据关卡ID从Luban中读取相应配置表，并获取关卡相关配置
        // 本脚本核心功能为储存关卡ID
        # endregion

        public int levelId;
        public int playerCardDeckId;

        // 关卡信息获取器
        public LevelBattleItem GetLevelInfo()
        {
            if(!LubanLoader.IsInit) LubanLoader.Init();
            
            var info = LubanLoader.tables.LevelBattleItems.Get(levelId);
            return info;
        }
    }
}
