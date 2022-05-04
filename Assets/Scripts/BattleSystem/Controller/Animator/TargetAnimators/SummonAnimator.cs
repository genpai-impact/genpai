using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class SummonAnimator : TargetAnimator
    {
        GameObject unitObject;

        public SummonAnimator(Unit _unit, GameObject _unitObject) : base(_unit, AnimatorType.TargetAnimator.Summon)
        {
            unitObject = _unitObject;
        }

        public override void TargetAct()
        {
            unitObject.GetComponent<UnitDisplay>().Init(GetFreshUnitView());
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