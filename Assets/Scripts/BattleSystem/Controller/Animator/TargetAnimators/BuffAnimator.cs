using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于Buff相关显示
    /// </summary>
    public class AddBuffAnimator : TargetAnimator
    {

        public AddBuffAnimator(Unit unit) : base(unit, AnimatorType.TargetAnimator.AddBuff) { }

        public override void TargetAct()
        {
            UnitEntity.unitDisplay.Display(GetFreshUnitView());
            AudioManager.Instance.PlayerEffect("Effect_DeBuff");
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

        public DelBuffAnimator(Unit unit) : base(unit, AnimatorType.TargetAnimator.DelBuff) { }

        public override void TargetAct()
        {
            UnitEntity.unitDisplay.Display(GetFreshUnitView());
            
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