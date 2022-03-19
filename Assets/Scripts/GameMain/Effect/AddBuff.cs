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
        public UnitEntity source;
        public UnitEntity target;

        public BaseBuff buff;


        public AddBuff(UnitEntity _source, UnitEntity _target, BaseBuff _buff)
        {
            this.source = _source;
            this.target = _target;
            this.buff = _buff;
        }

        public UnitEntity GetSource()
        {
            return source;
        }

        public UnitEntity GetTarget()
        {
            return target;
        }

        public void Add()
        {
            BaseBuff index = target.buffAttachment.FirstOrDefault(buff => buff.buffName == this.buff.buffName);
            // 无Buff则添加
            if (index == null)
            {
                buff.AddBuff(target);
            }
            else
            {
                // 判断有Buff时是否叠层
                if (index is IBuffIncreasable)
                {
                    (index as IBuffIncreasable).IncreaseBuff((buff as IBuffIncreasable).GetIncrease());
                }
            }

            target.GetComponent<UnitDisplay>().FreshUnitUI();

        }
    }
}