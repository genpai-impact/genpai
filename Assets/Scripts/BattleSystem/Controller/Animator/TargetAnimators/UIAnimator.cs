using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class UIAnimator : TargetAnimator
    {


        public UIAnimator(Unit _unit) : base(_unit, AnimatorType.TargetAnimator.UI) { }

        public override void TargetAct()
        {
            unitEntity.UnitDisplay.FreshUnitUI(GetFreshUnitView());
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