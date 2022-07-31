
using cfg.level;
using UnityEngine;

namespace BattleSystem.Service.Mission
{
    public class MissionConfig
    {
        /// <summary>
        /// 起始手牌数量
        /// </summary>
        public int StartCardCount = 4;
        /// <summary>
        /// 起手英雄数量
        /// </summary>
        public int StartHeroCount = 2;
        /// <summary>
        /// 每回合抽卡数量
        /// </summary>
        public int RoundCardCount = 1;
        /// <summary>
        /// 角色上场CD
        /// </summary>
        public int CharaCD = 2;
        public int S_HandCardLimit = 10; // 手牌上限
        public int RoundMonsterCount = 1; // 每回合可以上几个怪 

        public readonly int BossID; // 当前Boss
        public readonly int EnemyCardLibraryId; // 敌人卡组选择
        public readonly int UserCardLibraryId; // 玩家卡组选择
        
        public MissionConfig(LevelBattleItem levelInfo, int playerInfo)
        {
            BossID = levelInfo.BossId;
            EnemyCardLibraryId = levelInfo.EnemyCardDeck;
            UserCardLibraryId = playerInfo;
            Debug.Log("User CardLibrary Usage:" + UserCardLibraryId);
            Debug.Log("Enemy CardLibrary Usage:" + EnemyCardLibraryId);
        }
        public MissionConfig(int startCardCount, int startHeroCount, int roundCardCount, int charaCD,
            int handCardLimit, int userCardLibraryId, int enemyCardLibraryId, int bossId, int roundMonsterCount)
        {
            StartCardCount = startCardCount;
            StartHeroCount = startHeroCount;
            RoundCardCount = roundCardCount;
            CharaCD = charaCD;
            S_HandCardLimit = handCardLimit;
            UserCardLibraryId = userCardLibraryId;
            EnemyCardLibraryId = enemyCardLibraryId;
            BossID = bossId;
            RoundMonsterCount = roundMonsterCount;
        }
    }
}
