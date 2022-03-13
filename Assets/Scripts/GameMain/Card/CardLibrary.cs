
using System.Collections.Generic;

/// <summary>
/// todo
/// 本文件的数据，应该可以以文件形式存储，并且在游戏启动时读取。
/// </summary>
namespace Genpai
{

    /// <summary>
    /// 用户的卡牌收藏
    /// </summary>
    public class CardLibrary : Singleton<CardLibrary>
    {
        public List<int> OwnCardIDList;//用户已经拥有的卡列表
        public Dictionary<int, UserCardDeck> UserCardDeck;// 用户的卡组
        public Dictionary<int, UserCardDeck> EnemyCardDeck;// 敌人的卡组

        /// <summary>
        /// 从本地文档读取
        /// </summary>
        public void LoadFormFile()
        {
            // todo 改成真正的从文件读取
            // 假设cardDeckId和cardDeck已经从本地文档读取成功（注意，文件里应该存储的是多个卡组）
            int cardDeckId = 0;
            List<int> cardDeck = new List<int> { 100, 101, 102, 103, 300, 301, 301, 200, 201, 301, 301, 202, 203, 301, 301, 204, 205, 206, 207 };
            UserCardDeck = new Dictionary<int, UserCardDeck>();
            UserCardDeck.Add(cardDeckId, new UserCardDeck(cardDeckId, cardDeck));

            int enemyCardDeckId = 0;
            List<int> enemyCardDeck = new List<int> { 100, 101, 102, 103, 300, 301, 301, 200, 201, 301, 301, 202, 203, 301, 301, 204, 205, 206, 207 };
            EnemyCardDeck = new Dictionary<int, UserCardDeck>();
            EnemyCardDeck.Add(enemyCardDeckId, new UserCardDeck(enemyCardDeckId, enemyCardDeck));
        }
    }

    /// <summary>
    /// 用户的卡组
    /// </summary>
    public class UserCardDeck
    {
        public int CardDeckID;
        public List<int> CardIdList;

        public UserCardDeck(int cardDeckID, List<int> cardIdList)
        {
            CardDeckID = cardDeckID;
            CardIdList = cardIdList;
        }
    }
}
