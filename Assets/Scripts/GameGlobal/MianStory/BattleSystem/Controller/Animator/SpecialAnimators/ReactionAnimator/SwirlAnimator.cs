using BattleSystem.Service.Element;

namespace BattleSystem.Controller.Animator.SpecialAnimators.ReactionAnimator
{
    public class SwrilAnimator : ReactionAnimator
    {

        public SwrilAnimator(Service.Unit.Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Swirl;
        }

        public override void SpecialAct()
        {
            // 待Prefab实现
            base.SpecialAct();
            // AudioManager.Instance.PlayerEffect("Effect.Swirl");
        }

        public override bool IsAnimationFinished()
        {
            return true;
        }

        public override void ShutDownAct()
        {
            // TODO: 
        }
    }
}