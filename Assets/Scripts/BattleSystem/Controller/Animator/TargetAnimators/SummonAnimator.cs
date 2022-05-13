using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// </summary>
    public class SummonAnimator : TargetAnimator
    {
        public readonly GameObject UnitObject;

        public SummonAnimator(Unit unit, GameObject unitObject) : base(unit, AnimatorType.TargetAnimator.Summon)
        {
            UnitObject = unitObject;
        }

        public override void TargetAct()
        {
            UnitObject.GetComponent<UnitDisplay>().Init(GetFreshUnitView());
            UnitObject.SetActive(true);
            AudioManager.Instance.PlayerEffect("Effect.Summon");
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