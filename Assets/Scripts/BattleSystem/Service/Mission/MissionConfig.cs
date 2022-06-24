
using cfg.level;

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

        public int BossID = 300; // 当前Boss
        public int EnemyCardDeckId = 0; // 敌人卡组选择
        public int UserCardDeckId = 0; // 玩家卡组选择
        
        public MissionConfig(LevelBattleItem levelInfo, int playerInfo)
        {
            BossID = levelInfo.BossId;
            EnemyCardDeckId = levelInfo.EnemyCardDeck;
            UserCardDeckId = playerInfo;
        }
        public MissionConfig(int startCardCount, int startHeroCount, int roundCardCount, int charaCD,
            int handCardLimit, int userCardDeckId, int enemyCardDeckId, int bossId, int roundMonsterCount)
        {
            StartCardCount = startCardCount;
            StartHeroCount = startHeroCount;
            RoundCardCount = roundCardCount;
            CharaCD = charaCD;
            S_HandCardLimit = handCardLimit;
            UserCardDeckId = userCardDeckId;
            EnemyCardDeckId = enemyCardDeckId;
            BossID = bossId;
            RoundMonsterCount = roundMonsterCount;
        }
    }
}
