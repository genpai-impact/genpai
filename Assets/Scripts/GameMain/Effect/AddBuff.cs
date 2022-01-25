using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 记录添加Buff的效果
    /// </summary>
    public class AddBuff : IEffect
    {
        public UnitEntity sourceUnit;
        public UnitEntity targetUnit;

        public int BuffID;
        public AddBuff(int _buffID)
        {
            this.BuffID = _buffID;
        }
    }
}