using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class FallAnimator : TargetAnimator
    {
        public FallAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator) : base(_unit, _targetAnimator)
        {
        }

        public FallAnimator(Unit _unit) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.Fall;
        }

        public override void TargetAct()
        {
            unitEntity.GetComponent<UnitDisplay>().Init(null);
            // unitEntity.UnitDisplay.Init(null);
        }

        public override bool IsAnimationFinished()
        {
            return base.IsAnimationFinished();
        }
    }
}