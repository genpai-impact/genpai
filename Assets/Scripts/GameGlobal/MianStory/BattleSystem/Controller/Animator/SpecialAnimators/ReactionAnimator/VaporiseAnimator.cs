using BattleSystem.Service.Element;
using Utils;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class VaporiseAnimator : ReactionAnimator
    {

        public VaporiseAnimator(Service.Unit.Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Vaporise;
            FeatureTypeEnum = AnimatorType.AnimatorTypeEnum.TargetAnimator;
        }

        public override void SpecialAct()
        {
            base.SpecialAct();
            AudioManager.Instance.PlayerEffect("Effect_Vaporise");
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