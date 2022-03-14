using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    class Cure : IEffect
    {

        public UnitEntity source;
        public UnitEntity target;
        public int cureValue;

        public UnitEntity GetSource()
        {
            return source;
        }

        public UnitEntity GetTarget()
        {
            return target;
        }

        public Cure(UnitEntity _source,UnitEntity _target,int _cureValue)
        {
            source = _source;
            target = _target;
            cureValue = _cureValue;
        }

        public void CureUnit()
        {
            target.Cured(cureValue);
        }
    }
}
