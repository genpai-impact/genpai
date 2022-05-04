using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Cure : IEffect
    {

        public Unit source;
        public Unit target;
        public int cureValue;

        public Unit GetSource()
        {
            return source;
        }

        public Unit GetTarget()
        {
            return target;
        }

        public Cure(Unit _source, Unit _target, int _cureValue)
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
