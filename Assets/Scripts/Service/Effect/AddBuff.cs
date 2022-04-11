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
        public NewUnit source;
        public NewUnit target;

        public BaseBuff buff;


        public AddBuff(NewUnit _source, NewUnit _target, BaseBuff _buff)
        {
            this.source = _source;
            this.target = _target;
            this.buff = _buff;
        }

        public NewUnit GetSource()
        {
            return source;
        }

        public NewUnit GetTarget()
        {
            return target;
        }

        public void Add()
        {
            BaseBuff index = target.buffAttachment.FirstOrDefault(buff => buff.buffName == this.buff.buffName);
            // 无Buff  或  新Buff是ATKBuff
            if (index == null || buff is BaseATKEnhanceBuff)
            {
                // Fixme
                //buff.AddBuff(target);
            }
            else
            {
                // 判断有Buff时是否叠层
                if (index is IBuffIncreasable)
                {
                    (index as IBuffIncreasable).IncreaseBuff((buff as IBuffIncreasable).GetIncrease());
                }
            }

            //target.GetComponent<UnitDisplay>().FreshUnitUI();

        }
    }
}