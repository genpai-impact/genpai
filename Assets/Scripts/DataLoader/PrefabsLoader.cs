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
        public GameObject cardPrefab;
        public GameObject charaPrefab;
        public GameObject unitPrefab;

        public GameObject cardPool;
        public GameObject cardHeap;
        public GameObject charaPool;


        public GameObject card2Pool;
        public GameObject card2Heap;
        public GameObject chara2Pool;

    }
}
