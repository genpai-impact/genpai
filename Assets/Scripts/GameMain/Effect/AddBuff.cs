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
        public UnitEntity source;
        public UnitEntity target;

        public int BuffID;
        public AddBuff(UnitEntity _source, UnitEntity _target, int _buffID)
        {
            this.source = _source;
            this.target = _target;
            this.BuffID = _buffID;
        }

        public UnitEntity GetSource()
        {
            return source;
        }

        public UnitEntity GetTarget()
        {
            return target;
        }
    }
}