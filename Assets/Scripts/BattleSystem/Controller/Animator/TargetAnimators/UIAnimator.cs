using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class UIAnimator : TargetAnimator
    {


        public UIAnimator(Unit unit) : base(unit, AnimatorType.TargetAnimator.UI) { }

        public override void TargetAct()
        {
            UnitEntity.unitDisplay.Display(GetFreshUnitView());
        }

        public override bool IsAnimationFinished()
        {
            return true;
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }
}