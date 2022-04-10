﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Genpai
{
    /// <summary>
    /// 记录添加Buff的效果
    /// </summary>
    public class DelBuff : IEffect
    {
        public NewUnit source;
        public NewUnit target;

        public BuffEnum BuffID;
        public int BuffNum;
        public DelBuff(NewUnit _source, NewUnit _target, BuffEnum _buffID, int _num = 1)
        {
            this.source = _source;
            this.target = _target;
            this.BuffID = _buffID;
            this.BuffNum = _num;
        }

        public NewUnit GetSource()
        {
            return source;
        }

        public NewUnit GetTarget()
        {
            return target;
        }

        public void Remove()
        {
            BaseBuff index = target.buffAttachment.FirstOrDefault(buff => buff.buffName == BuffID);

            if (index != null && index is IBuffDeleteable)
            {
                (index as IBuffDeleteable).DeleteBuff(BuffNum);
            }

            //target.GetComponent<UnitDisplay>().FreshUnitUI();
        }
    }
}