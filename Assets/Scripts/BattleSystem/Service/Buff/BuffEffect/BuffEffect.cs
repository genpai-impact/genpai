using System;
using System.Collections.Generic;

namespace Genpai
{
    public static partial class BuffEffect
    {
        public static void DamageOverTime(BuffPair buffPair, ref List<IEffect> dot)
        {
            if (!buffPair.IsWorking) return;
            var elementEnum = EnumUtil.ToEnum<ElementEnum>(buffPair.Buff.BuffAppendix);
            
            dot.Add(new Damage(null,buffPair.Unit,new DamageStruct(buffPair.Buff.Storey,elementEnum)));
        }
    }
}