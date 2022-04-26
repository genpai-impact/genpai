using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于Buff相关显示
    /// </summary>
    public class AddBuffAnimator : TargetAnimator
    {
        public AddBuffAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator) : base(_unit, _targetAnimator)
        {
            
        }

        public AddBuffAnimator(Unit _unit, Damage _damage) : base(_unit)
        {
           targetAnimatorType = AnimatorType.TargetAnimator.AddBuff;
        }

        public override void TargetAct()
        {
            unitEntity.UnitDisplay.FreshUnitUI(unitEntity.UnitModelDisplay.unitView);
        }

        public override bool IsAnimationFinished()
        {
            return base.IsAnimationFinished();
        }
    }

    public class DelBuffAnimator : TargetAnimator
    {
        public DelBuffAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator) : base(_unit, _targetAnimator)
        {
        }

        public DelBuffAnimator(Unit _unit) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.DelBuff;
        }

        public override void TargetAct()
        {
            unitEntity.UnitDisplay.FreshUnitUI(unitEntity.UnitModelDisplay.unitView);
        }

        public override bool IsAnimationFinished()
        {
            return base.IsAnimationFinished();
        }
    }
}