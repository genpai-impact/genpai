using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Genpai
{
    class processtest : MonoSingleton<processtest>
    {
        public GameObject cardPrefab;
        public GameObject cardPool;


        private void Awake()
        {

        }
        private void Start()
        {
            ProcessGameStart processGameStart = ProcessGameStart.GetInstance();
            processGameStart.Run();

        }
        private void Update()
        {

        }
    }
}
