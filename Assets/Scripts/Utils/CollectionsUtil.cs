using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public sealed class CollectionsUtil
    {
        private CollectionsUtil()
        {
        }
        public static void FisherYatesShuffle<T>(List<T> list)
        {
            List<T> cache = new List<T>();
            int currentIndex;
            while (list.Count > 0)
            {
                currentIndex = Random.Range(0, list.Count);
                cache.Add(list[currentIndex]);
                list.RemoveAt(currentIndex);
            }
            for (int i = 0; i < cache.Count; i++)
            {
                list.Add(cache[i]);
            }
        }
    }
}
