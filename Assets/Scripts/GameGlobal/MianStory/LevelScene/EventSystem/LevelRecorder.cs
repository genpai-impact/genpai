using System.Collections.Generic;
using System.Linq;
using cfg.level;
using DataScripts;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;


/// <summary>
/// 关卡通过情况
/// </summary>
public static class LevelRecorder
{
    public static List<LevelUnlocker> UserUnlockers = new List<LevelUnlocker>();

    public static bool Inited = false;


    public enum LevelAchieve { 
        Locked,//未解锁
        Zero,//解锁或0星
        One,//一星
        Two,//二星
        Three
    }

    //获取存档 ret：{关卡id，分数}
    public static Dictionary<int, int> GetLevelsSave(List<int> leveIds)
    {
        Dictionary<int,int> ret = new Dictionary<int,int>();
        foreach (int id in leveIds) {
            int score = LubanLoader.GetTables().LevelsSave.GetOrDefault(id).Achievement;
            ret.Add(id,score);
        }
        return ret;
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
