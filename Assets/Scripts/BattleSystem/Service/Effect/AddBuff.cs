using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Genpai
{
    /// <summary>
    /// 记录添加Buff的效果
    /// </summary>
    public class AddBuff : IEffect
    {
        public readonly Unit Source;
        public readonly Unit Target;

        public readonly BaseBuff Buff;


        public AddBuff(Unit source, Unit target, BaseBuff buff)
        {
            this.Source = source;
            this.Target = target;
            this.Buff = buff;
        }

        public Unit GetSource()
        {
            return Source;
        }

        public Unit GetTarget()
        {
            return Target;
        }

        public void Add()
        {
            BaseBuff index = Target.BuffAttachment.FirstOrDefault(buff => buff.BuffName == this.Buff.BuffName);

            switch (index)
            {
                // 无Buff  或  新Buff是ATKBuff
                case null:
                    Buff.AddBuff(Target);
                    break;
                // 判断有Buff时是否叠层
                case IBuffIncreasable increaseAble:
                    increaseAble.IncreaseBuff(((IBuffIncreasable)Buff).GetIncrease());
                    break;
            }
        }
    }
}