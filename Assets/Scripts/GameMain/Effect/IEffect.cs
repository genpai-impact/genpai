using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 基础效果接口，通过继承其表示自身作为效果
    /// </summary>
    public interface IEffect
    {
        public UnitEntity GetSource();
        public UnitEntity GetTarget();
    }
}