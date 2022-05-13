using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// Buff快速视图，概括信息
    /// </summary>
    public class BuffView
    {
        public readonly BuffEnum BuffName;
        public BuffType BuffType;
        public Image BuffImage;
        public int LifeCycles;  // Buff生命周期
        public int Storey;      // Buff层数

        public BuffView(BaseBuff baseBuff)
        {
            BuffName = baseBuff.BuffName;
            BuffType = baseBuff.BuffType;

        }

        public string ReturnDescription()
        {
            const string ret = "九层的究↗极↘Buff在九回合后消失";
            return ret;
        }
    }
}