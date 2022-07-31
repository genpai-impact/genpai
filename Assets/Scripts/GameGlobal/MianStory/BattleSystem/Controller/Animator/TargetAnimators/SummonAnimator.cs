using BattleSystem.Controller.Unit;
using UnityEngine;
using Utils;

namespace BattleSystem.Controller.Animator.TargetAnimators
{
    /// <summary>
    /// </summary>
    public class SummonAnimator : TargetAnimator
    {
        public readonly GameObject UnitObject;

        public SummonAnimator(Service.Unit.Unit unit, GameObject unitObject) : base(unit, AnimatorType.TargetAnimator.Summon)
        {
            UnitObject = unitObject;
        }

        public override void TargetAct()
        {
            UnitObject.GetComponent<UnitDisplay>().Display(GetFreshUnitView());
            UnitObject.SetActive(true);
            AudioManager.Instance.PlayerEffect("Effect_Summon");
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