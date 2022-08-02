﻿using System.Collections.Generic;
using System.Linq;
using cfg.level;
using DataScripts;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;


/// <summary>
/// 关卡通过情况
/// </summary>
public static class LevelLocker
{
    public static List<LevelUnlocker> UserUnlockers = new List<LevelUnlocker>();

    public static bool Inited = false;

    enum LevelAchieve { 
        Locked,
        Zero,
        One,
        Two,
        Three
    }

    public Dictionary<string, LevelAchieve> GetLevelsSave()
    {

    }

    public static bool CheckUnlock(int levelId, bool levelOrEvent = true)
    {
        // TODO: 通过玩家信息中更新Unlocker列表

        var unlockers = levelOrEvent
            ? LubanLoader.GetTables().LevelItems.GetOrDefault(levelId).LevelUnlockers
            : LubanLoader.GetTables().EventItems.GetOrDefault(levelId).EventUnlockers;


        foreach (var unlocker in unlockers)
        {
            // Debug.Log("show unlocker "+unlocker.UnlockType+" "+unlocker.UnlockCondition+" "+unlocker.ExtraCondition);

            if (!UserUnlockers.Exists(levelUnlocker => CompareUnlocker(levelUnlocker, unlocker))) return false;

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
