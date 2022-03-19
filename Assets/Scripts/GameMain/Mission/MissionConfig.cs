
namespace Genpai
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
        public int UserCardDeckId = 0; // 用户选了哪套卡组
        public int EnemyCardDeckId = 0; // 敌人选了哪套卡组
        public int BossID = 401; // 本局boss
        public int RoundMonsterCount = 1; // 每回合可以上几个怪 

        public MissionConfig()
        {
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
