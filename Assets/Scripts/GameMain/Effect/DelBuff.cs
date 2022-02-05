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
        public DelBuff(UnitEntity _source, UnitEntity _target, BuffEnum _buffID, int _num = 1)
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

            BaseBuff index = target.buffAttachment.FirstOrDefault(buff => buff.buffName == BuffID);

            if (!index.Equals(null) && index is IBuffDeleteable)
            {
                IBuffDeleteable deleteable = index as IBuffDeleteable;

                // 唤醒Buff减层函数，返回值为是否完全销毁
                if (deleteable.DeleteBuff(BuffNum))
                {
                    target.buffAttachment.Remove(index);
                }
            }
        }
    }
}