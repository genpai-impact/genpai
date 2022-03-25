using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    class CardDeckLoader : Singleton<CardDeckLoader>
    {
        private string UserCardDeckPath = "Data\\UserCardDeck";
        private string EnemyCardDeckPath = "Data\\EnemyCardDeck";

        public List<UserCardDeck> UserCardDeckList = new List<UserCardDeck>();
        public List<UserCardDeck> EnemyCardDeckList = new List<UserCardDeck>();

        public void CardDeckLoad()
        {
            UserCardDeckLoad();
            EnemyCardDeckLoad();
        }

        public void UserCardDeckLoad()
        {
            TextAsset text = Resources.Load(UserCardDeckPath) as TextAsset;
            Debug.Log(text == null);
            string[] textSplit = text.text.Split('\n');
            int CardDeckID = 0;
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                List<int> CardIdList = new List<int>();
                foreach (var split in lineSplit)
                {
                    CardIdList.Add(int.Parse(split));
                }
                UserCardDeckList.Add(new UserCardDeck(CardDeckID, CardIdList));
                CardDeckID++;
            }
        }

        public void EnemyCardDeckLoad()
        {
            TextAsset text = Resources.Load(EnemyCardDeckPath) as TextAsset;
            string[] textSplit = text.text.Split('\n');
            int CardDeckID = 0;
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                List<int> CardIdList = new List<int>();
                foreach (var split in lineSplit)
                {
                    CardIdList.Add(int.Parse(split));
                }
                EnemyCardDeckList.Add(new UserCardDeck(CardDeckID, CardIdList));
                CardDeckID++;
            }
        }

        public Dictionary<int, UserCardDeck> GetUserCardDeck()
        {
            Dictionary<int, UserCardDeck> UserCardDeck = new Dictionary<int, UserCardDeck>();
            foreach (var deck in UserCardDeckList)
            {
                UserCardDeck.Add(deck.CardDeckID, deck);
            }
            return UserCardDeck;
        }

        public Dictionary<int, UserCardDeck> GetEnemyCardDeck()
        {
            Dictionary<int, UserCardDeck> EnemyCardDeck = new Dictionary<int, UserCardDeck>();
            foreach (var deck in EnemyCardDeckList)
            {
                EnemyCardDeck.Add(deck.CardDeckID, deck);
            }
            return EnemyCardDeck;
        }
    }
}
