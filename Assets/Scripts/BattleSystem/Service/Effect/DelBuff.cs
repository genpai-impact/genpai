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
        public readonly Unit Source;
        public readonly Unit Target;

        public readonly BuffEnum BuffID;
        public readonly int BuffNum;
        public DelBuff(Unit source, Unit target, BuffEnum buffID, int num = 1)
        {
            this.Source = source;
            this.Target = target;
            this.BuffID = buffID;
            this.BuffNum = num;
        }

        public Unit GetSource()
        {
            return Source;
        }

        public Unit GetTarget()
        {
            return Target;
        }

        public void Remove()
        {
            BaseBuff index = Target.BuffAttachment.FirstOrDefault(buff => buff.BuffName == BuffID);

            if (index is IBuffDeleteable deleteAble)
            {
                deleteAble.DeleteBuff(BuffNum);
            }

            //target.GetComponent<UnitDisplay>().FreshUnitUI();
        }
    }
}