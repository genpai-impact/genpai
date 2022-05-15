using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;
using SimpleJSON;
using System.IO;

namespace Genpai
{
    public static class LubanLoader
    {
        public static Tables tables;

        public static void Init()
        {
            tables = new Tables(Loader);

            // cfg.card.CardItem item = tables.CardItems.Get(101);
            // Debug.Log($"{item.CardName}   {item.CardType}");
            CardLoader.Instance.Init();
        

        }
         
        private static JSONNode Loader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.dataPath + "/Resources/LubanDataJson/" + fileName + ".json"));
        }

    }
}

