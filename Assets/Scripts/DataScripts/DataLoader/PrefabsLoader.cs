using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
