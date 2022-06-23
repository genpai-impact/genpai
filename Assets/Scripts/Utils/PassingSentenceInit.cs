using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class PassingSentenceInit : Singleton<PassingSentenceInit>
    {

        public List<string> SentenceList;
        PassingSentenceInit()
        {
            SentenceList = new List<string>();
        }

        public void ReadSentence(string path)
        {

            TextAsset text = Resources.Load(path) as TextAsset;
            Debug.Log(text == null);
            string allText = text.text;
            string readData = allText.Substring(allText.IndexOf("[") + 1, allText.IndexOf("]") - allText.IndexOf("[") - 1);
            string[] splitDatas = readData.Split('\n');
            //foreach (var i in splitDatas) Debug.Log(i);
            foreach (var splitData in splitDatas)
            {
                int start = splitData.IndexOf('"') + 1;
                int end = splitData.IndexOf('。');
                //Debug.Log(start + " " + end);
                //Debug.Log(splitData);
                SentenceList.Add(splitData.Substring(start, end - start));

            }


        }


    }
}
