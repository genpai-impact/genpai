using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 元素枚举
    /// </summary>
    public enum ElementEnum
    {
        None=0x00,       // 无
        Pyro=0x01,       // 火
        Hydro=0x02,      // 水
        Cryo=0x04,       // 冰
        Electro=0x08,    // 雷
        Anemo=0x10,      // 风
        Geo=0x20,        // 岩
    }

    public enum ElementReactionEnum
    {
        None,
        Swirl,          // Anemo + ?        扩散
        Crystallise,    // Geo + ?          结晶
        Overload,       // Electro + Pyro   超载
        Superconduct,   // Electro + Cryo   超导
        ElectroCharge,  // Electro + Hydro  感电
        Melt,           // Pyro + Cryo      融化
        Vaporise,       // Pyro + Hydro     蒸发
        Freeze,         // Cryo + Hydro     冻结
        Burning,        // Dendro + Pyro    燃烧
    }

}