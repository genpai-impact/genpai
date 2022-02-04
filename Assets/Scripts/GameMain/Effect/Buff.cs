using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class Buff : BaseBuff
    {
        public BuffEnum BuffType;

        //Buff层数,用于引燃和护盾
        public int BuffNums;

        public Buff(BuffEnum _buff)
        {
            BuffType = _buff;
            BuffNums = 1;
        }

        public Buff(BuffEnum _buff,int num)
        {
            BuffType = _buff;
            BuffNums = num;
        }

    }
}
