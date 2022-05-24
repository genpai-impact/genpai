using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class Cure : IEffect
    {

        public readonly Unit Source;
        public readonly Unit Target;
        public readonly int CureValue;

        public Unit GetSource()
        {
            return Source;
        }

        public Unit GetTarget()
        {
            return Target;
        }

        public Cure(Unit source, Unit target, int cureValue)
        {
            Source = source;
            Target = target;
            CureValue = cureValue;
        }

        public void CureUnit()
        {
            Target.Cured(CureValue);
        }
    }
}
