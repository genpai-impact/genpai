using System.Collections.Generic;
using System.Linq;
using cfg.level;
using DataScripts;
using Packages.Rider.Editor.UnitTesting;

namespace GameSystem.LevelSystem
{
    /// <summary>
    /// 关卡解锁检查器
    /// </summary>
    public static class LevelUnlockChecker
    {
        private static List<LevelUnlocker> _userUnlockers;

        public static void TestInit()
        {
             _userUnlockers = new List<LevelUnlocker>
            {
                new LevelUnlocker(UnlockType.Event, 2000, true),
                new LevelUnlocker(UnlockType.Event, 2001, true)
            };
        }
        
        public static bool CheckUnlock(int levelId)
        {
            // TODO: 通过玩家信息中更新Unlocker列表
            
            var unlockers = LubanLoader.GetTables().LevelItems.Get(levelId).LevelUnlockers;

            return unlockers.All(unlocker => _userUnlockers.Contains(unlocker));
        }
        
    }
}