using BattleSystem.Service.Element;
using Utils;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class OverloadAnimator : ReactionAnimator
    {

        public OverloadAnimator(Service.Unit.Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Overload;
        }

        public override void SpecialAct()
        {
            base.SpecialAct();
            AudioManager.Instance.PlayerEffect("Effect_Overload");
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