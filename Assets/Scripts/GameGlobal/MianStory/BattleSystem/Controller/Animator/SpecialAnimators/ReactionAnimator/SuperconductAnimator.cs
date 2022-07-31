using BattleSystem.Service.Element;
using Utils;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class SuperconductAnimator : ReactionAnimator
    {

        public SuperconductAnimator(Service.Unit.Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Superconduct;
        }

        public override void SpecialAct()
        {
            base.SpecialAct();
            AudioManager.Instance.PlayerEffect("Effect_Superconduct");
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