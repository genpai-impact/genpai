using BattleSystem.Service.Element;
using Utils;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class ElectroChargeAnimator : ReactionAnimator
    {

        public ElectroChargeAnimator(Service.Unit.Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.ElectroCharge;
        }

        public override void SpecialAct()
        {
            base.SpecialAct();
            AudioManager.Instance.PlayerEffect("Effect_ElectroCharge");
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