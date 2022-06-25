using System.Collections.Generic;
using System.Linq;
using cfg.level;
using DataScripts;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

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
                new LevelUnlocker(UnlockType.Event, 2000, 1),
                new LevelUnlocker(UnlockType.Event, 2001, 1)
            };
        }
        
        public static bool CheckUnlock(int levelId)
        {
            // TODO: 通过玩家信息中更新Unlocker列表
            TestInit();
            
            var unlockers = LubanLoader.GetTables().LevelItems.Get(levelId).LevelUnlockers;
            
            foreach (var unlocker in unlockers)
            {
                // Debug.Log("show unlocker "+unlocker.UnlockType+" "+unlocker.UnlockCondition+" "+unlocker.ExtraCondition);
                
                if (!_userUnlockers.Exists(levelUnlocker => CompareUnlocker(levelUnlocker,unlocker))) return false;

            }

            return true;
        }

        public static bool CompareUnlocker(LevelUnlocker unlocker1, LevelUnlocker unlocker2)
        {
            if (unlocker1.UnlockType != unlocker2.UnlockType) return false;
            if (unlocker1.UnlockCondition != unlocker2.UnlockCondition) return false;
            if (unlocker1.ExtraCondition != unlocker2.ExtraCondition) return false;
            return true;
        }
        
    }
}