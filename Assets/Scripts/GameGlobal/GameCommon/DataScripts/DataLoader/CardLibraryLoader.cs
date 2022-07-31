using System.Collections.Generic;
using BattleSystem.Service.Card;
using cfg.level;
using UnityEngine;
using Utils;
using SimpleJSON;
using System.IO;
using BattleSystem.Service.Player;

namespace DataScripts.DataLoader
{
    class CardLibraryLoader : Singleton<CardLibraryLoader>
    {
        
        public CardLibraries CardLibraries { get; private set; }
        public CardLibraries UserCardLibraries { get; private set; }
        
        
        public void CardDeckLoad()
        {
            CardLibraries = LubanLoader.GetTables().CardLibraries;
            
            // todo: 设置本地卡组位置
            string cardLibraryLocation = Application.streamingAssetsPath + "/LubanDataJson/level_cardlibraries.json";
            
            UserCardLibraries = new CardLibraries(JSON.Parse(File.ReadAllText(cardLibraryLocation)));

            // Debug.Log(UserCardLibraries.Get(50000).Desc);

        }

        public CardLibrary GetCardLibrary(BattleSite site, int libraryId)
        {
            return site switch
            {
                BattleSite.P1 => UserCardLibraries.Get(libraryId),
                BattleSite.P2 => CardLibraries.Get(libraryId),
                _ => null
            };
        }
        
    }


}
