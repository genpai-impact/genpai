using System.Collections.Generic;
using BattleSystem.Service.Card;
using cfg.level;
using UnityEngine;
using Utils;

namespace DataScripts.DataLoader
{
    class CardLibraryLoader : Singleton<CardLibraryLoader>
    {
        
        public CardLibraries CardLibraries;

        public void CardDeckLoad()
        {
            
            CardLibraries = LubanLoader.GetTables().CardLibraries;
        }
        
    }
}
