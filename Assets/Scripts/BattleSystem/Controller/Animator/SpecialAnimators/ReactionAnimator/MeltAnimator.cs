using BattleSystem.Service.Element;
using Utils;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class MeltAnimator : ReactionAnimator
    {

        public MeltAnimator(Service.Unit.Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Melt;
            FeatureTypeEnum = AnimatorType.AnimatorTypeEnum.TargetAnimator;
        }

        public override void SpecialAct()
        {
            base.SpecialAct();
            AudioManager.Instance.PlayerEffect("Effect_Melt");
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