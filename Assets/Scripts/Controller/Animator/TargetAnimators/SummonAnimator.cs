using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class SummonAnimator : TargetAnimator
    {
        public SummonAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator) : base(_unit, _targetAnimator)
        {
        }

        public SummonAnimator(Unit _unit) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.Summon;
        }

        public override void TargetAct()
        {
        }

        public override bool IsAnimationFinished()
        {
            return base.IsAnimationFinished();
        }
    }
}