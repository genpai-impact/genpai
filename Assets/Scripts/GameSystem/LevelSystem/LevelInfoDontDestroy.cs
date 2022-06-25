using System.Collections.Generic;
using cfg.level;
using DataScripts;
using UnityEngine.SceneManagement;
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
            
            var info = LubanLoader.GetTables().LevelBattleItems.Get(levelId);
            return info;
        }

        /// <summary>
        /// 完成关卡后Call的查找奖励
        /// </summary>
        /// <returns></returns>
        public List<Reward> GetReward()
        {
            return LubanLoader.GetTables().LevelItems.Get(levelId).Rewards;
        }

        /// <summary>
        /// 完成关卡后Call的场景接续
        /// </summary>
        public void VictoryLoadScene()
        {
            string targetScene = LubanLoader.GetTables().LevelItems.Get(levelId).Story2;
            // 读取对应故事场景
            SceneManager.LoadScene(targetScene);
        }

        /// <summary>
        /// 完成之后Call，为玩家设定关卡解锁标识物
        /// </summary>
        /// <param name="flag"></param>
        public void SetUnlocker(bool flag = true)
        {
            LevelUnlocker unlocker = new LevelUnlocker(UnlockType.Level, levelId, flag);
            
        }
    }
}
