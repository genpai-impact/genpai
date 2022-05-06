
using UnityEngine;

namespace Genpai
{
    class PrefabsLoader : MonoSingleton<PrefabsLoader>
    {
        [Header("Prefabs")]
        public GameObject cardPrefab;
        public GameObject chara_cardPrefab;
        public GameObject chara_BannerPrefab;
        public GameObject unitPrefab;
        public GameObject spellPrefab;

        [Header("P1布局")]
        public GameObject cardPool;
        public GameObject cardHeap;
        public GameObject charaPool;
        public GameObject charaBannerOnBattle;

        [Header("P2布局")]
        public GameObject card2Pool;
        public GameObject card2Heap;
        public GameObject chara2Pool;
        public GameObject charaBanner2OnBattle;

        [Header("总体布局")]
        public GameObject infoCard;

    }
}
