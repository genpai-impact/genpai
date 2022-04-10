using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    class Cure : IEffect
    {

        public NewUnit source;
        public NewUnit target;
        public int cureValue;

        public NewUnit GetSource()
        {
            return source;
        }

        public NewUnit GetTarget()
        {
            return target;
        }

        public Cure(NewUnit _source, NewUnit _target, int _cureValue)
        {
            source = _source;
            target = _target;
            cureValue = _cureValue;
        }

        public void CureUnit()
        {
            // TODO：实现恢复
            // target.Cured(cureValue);
        }
    }
}
