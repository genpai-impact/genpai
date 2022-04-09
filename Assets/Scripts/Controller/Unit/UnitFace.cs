using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 可用于展示的本地单位数据表
    /// </summary>
    public class UnitFace
    {

        public string unitName;
        public UnitType unitType;

        public List<string> buffList;

        // >>> 单位面板
        public int HP;
        public int ATK;
        public ElementEnum ATKElement;
        public ElementEnum SelfElement;


    }
}