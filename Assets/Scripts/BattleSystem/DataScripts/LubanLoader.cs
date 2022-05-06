using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;
using SimpleJSON;
using System.IO;

namespace Genpai
{
    public class LubanLoader : MonoBehaviour
    {
        public Tables tables;

        void Awake()
        {
            tables = new Tables(Loader);

            cfg.card.CardItem item = tables.CardItems.Get(101);
            // Debug.Log($"{item.CardName}   {item.CardType}");
        }

        private JSONNode Loader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.dataPath + "/Resources/LubanDataJson/" + fileName + ".json"));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

