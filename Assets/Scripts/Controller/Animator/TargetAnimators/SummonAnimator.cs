using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class SummonAnimator : TargetAnimator
    {
        GameObject unitObject;
        
        public SummonAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator, GameObject _unitObject) : base(_unit, _targetAnimator)
        {
            unitObject = _unitObject;
        }

        public SummonAnimator(Unit _unit, GameObject _unitObject) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.Summon;
            unitObject = _unitObject;
        }

        public override void TargetAct()
        {
            unitObject.GetComponent<UnitDisplay>().Init(unitEntity.GetUnit().GetView());
            unitObject.SetActive(true);
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