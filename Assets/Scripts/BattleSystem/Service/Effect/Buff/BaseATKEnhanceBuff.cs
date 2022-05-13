using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// ATKBuff基类
    /// </summary>
    public abstract class BaseAtkEnhanceBuff : BaseBuff
    {
        /// <summary>
        /// 层数
        /// <para>即atk增加的数值</para>
        /// </summary>
        public int Storey;

    }
}
