using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// ��¼���Buff��Ч��
    /// </summary>
    public class AddBuff : IEffect
    {
        public UnitEntity sourceUnit;
        public UnitEntity targetUnit;

        public int BuffID;
        public AddBuff(int _buffID)
        {
            this.BuffID = _buffID;
        }
    }
}