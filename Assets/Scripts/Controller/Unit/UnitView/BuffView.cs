using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// Buff快速视图，概括信息
    /// </summary>
    public class BuffView
    {
        public BuffEnum buffName;
        public BuffType buffType;

        public int lifeCycles;  // Buff生命周期
        public int storey;      // Buff层数

        public BuffView(BaseBuff _baseBuff)
        {
            buffName = _baseBuff.buffName;
            buffType = _baseBuff.buffType;

        }

        public string ReturnDescription()
        {
            string ret = "九层的究↗极↘Buff在九回合后消失";
            return ret;
        }
    }
}