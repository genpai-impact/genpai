using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于Buff相关显示
    /// </summary>
    public class AddBuffAnimator : TargetAnimator
    {

        public AddBuffAnimator(Unit _unit) : base(_unit, AnimatorType.TargetAnimator.AddBuff) { }

        public override void TargetAct()
        {
            UnitEntity.unitDisplay.FreshUnitUI(GetFreshUnitView());
        }

        public override bool IsAnimationFinished()
        {
            return base.IsAnimationFinished();
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }

    public class DelBuffAnimator : TargetAnimator
    {

        public DelBuffAnimator(Unit _unit) : base(_unit, AnimatorType.TargetAnimator.DelBuff) { }

        public override void TargetAct()
        {
            UnitEntity.unitDisplay.FreshUnitUI(GetFreshUnitView());
        }

        public override bool IsAnimationFinished()
        {
            return base.IsAnimationFinished();
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }
}