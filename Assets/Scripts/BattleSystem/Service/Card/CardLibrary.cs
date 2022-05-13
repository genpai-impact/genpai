﻿
using System.Collections.Generic;

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
            CardDeckLoader.Instance.CardDeckLoad();
            UserCardDeck = CardDeckLoader.Instance.GetUserCardDeck();
            EnemyCardDeck = CardDeckLoader.Instance.GetEnemyCardDeck();
        }
    }

    /// <summary>
    /// 用户的卡组
    /// </summary>
    public class UserCardDeck
    {
        public readonly int CardDeckID;
        public readonly List<int> CardIdList;

        public UserCardDeck(int cardDeckID, List<int> cardIdList)
        {
            CardDeckID = cardDeckID;
            CardIdList = cardIdList;
        }
    }
}
