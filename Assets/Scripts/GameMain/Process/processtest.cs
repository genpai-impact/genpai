using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Genpai
{
    class processtest : MonoBehaviour
    {
        public int time = 1;
        private void Awake()
        {

        }
        private void Start()
        {


        }
        private void Update()
        {
            if (time == 1)
            {

                ProcessGameStart processGameStart = ProcessGameStart.GetInstance();
                processGameStart.Run();
                time = 0;
            }

        }
    }
}
