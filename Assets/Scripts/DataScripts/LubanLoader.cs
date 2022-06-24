using System.IO;
using cfg;
using DataScripts.DataLoader;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Rendering;

namespace DataScripts
{
    public static class LubanLoader
    {
        private static Tables _tables;

        public static bool IsInit = false;

        public static void Init()
        {
            if (IsInit) return;
            
            _tables = new Tables(Loader);
            

            IsInit = true;
        }
         
        private static JSONNode Loader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.streamingAssetsPath + "/LubanDataJson/" + fileName + ".json"));
        }

        public static Tables GetTables()
        {
            if (!IsInit) Init();
            
            return _tables;
        }

    }
}

