using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Genpai
{
    /// <summary>
    /// 记录添加Buff的效果
    /// </summary>
    public class AddBuff : IEffect
    {
        public UnitEntity source;
        public UnitEntity target;

        public BuffEnum BuffID;
        public int BuffNum;
        //
        public AddBuff(UnitEntity _source, UnitEntity _target, BuffEnum _buffID,int _num=1)
        {
            this.source = _source;
            this.target = _target;
            this.BuffID = _buffID;
            this.BuffNum = _num;
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
            Buff index = target.buffAttachment.FirstOrDefault(buff => buff.BuffType == BuffID);
            if(index.Equals(null))
            {
                target.buffAttachment.AddLast(new Buff(BuffID,BuffNum));
            }
            else if(index.BuffType==BuffEnum.Burning)
            {
                index.BuffNums++;
            }
            else if(index.BuffType==BuffEnum.Shield)
            {
                index.BuffNums += BuffNum;
            }
        }
    }
}