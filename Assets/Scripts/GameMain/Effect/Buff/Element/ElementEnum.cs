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
        None,       // 无
        Pyro,       // 火
        Hydro,      // 水
        Cryo,       // 冰
        Electro,    // 雷
        Anemo,      // 风
        Geo,        // 岩
    }

    public enum ElementReactionEnum
    {
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