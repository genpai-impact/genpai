using System.IO;
using cfg;
using DataScripts.DataLoader;
using SimpleJSON;
using UnityEngine;

namespace DataScripts
{
    public static class LubanLoader
    {
        public static Tables tables;

        public static bool IsInit = false;

        public static void Init()
        {
            if (IsInit) return;
            
            tables = new Tables(Loader);

            // cfg.card.CardItem item = tables.CardItems.Get(101);
            // Debug.Log($"{item.CardName}   {item.CardType}");
            CardLoader.Instance.Init();

            IsInit = true;
        }
         
        private static JSONNode Loader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.streamingAssetsPath + "/LubanDataJson/" + fileName + ".json"));
        }

    }
}

