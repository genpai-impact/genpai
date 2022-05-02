using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class UIAnimator : TargetAnimator
    {
        public UIAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator) : base(_unit, _targetAnimator)
        {
        }

        public UIAnimator(Unit _unit) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.UI;
        }

        public override void TargetAct()
        {
            unitEntity.UnitModelDisplay.unitView.MP = ((Chara)(unitEntity.GetUnit())).MP;
            unitEntity.UnitDisplay.FreshUnitUI(unitEntity.UnitModelDisplay.unitView);
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