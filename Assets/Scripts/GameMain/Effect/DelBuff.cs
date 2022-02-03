using System.Collections;
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
        public UnitEntity source;
        public UnitEntity target;

        public BuffEnum BuffID;
        public int BuffNum;
        public DelBuff(UnitEntity _source, UnitEntity _target, BuffEnum _buffID,int _num=1)
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

        public void Remove()
        {
            //护盾Buff会依据伤害扣层数
            //引燃一次扣除所有层数，随引燃传入的num应该取无穷大
            //其余Buff在Add时上限一层，扣除一层后即移除
            Buff index = target.buffAttachment.FirstOrDefault(buff => buff.BuffType == BuffID);
            if(!index.Equals(null))
            {
                index.BuffNums -= BuffNum;
                if(index.BuffNums<=0)
                {
                    target.buffAttachment.Remove(index);
                }
            }
        }
    }
}