namespace BattleSystem.Controller.Animator.TargetAnimators
{
    /// <summary>
    /// </summary>
    public class UIAnimator : TargetAnimator
    {


        public UIAnimator(Service.Unit.Unit unit) : base(unit, AnimatorType.TargetAnimator.UI) { }

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