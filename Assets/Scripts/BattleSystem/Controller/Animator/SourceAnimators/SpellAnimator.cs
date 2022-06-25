using BattleSystem.Service.Common;
using Utils;

namespace BattleSystem.Controller.Animator.SourceAnimators
{
    public class SpellAnimator : SourceAnimator
    {
        private static readonly int Skill = UnityEngine.Animator.StringToHash("skill");
        public SpellAnimator(Service.Unit.Unit unit) : base(unit, AnimatorType.SourceAnimator.Spell) { }

        public override void SourceAct()
        {
            if (IsTriggerExist(Animator, "skill"))
            {
                AnimationHandle.Instance.AddAnimator("skill", Animator);

                Animator.SetTrigger(Skill);
            }
            
            AudioManager.Instance.PlayerEffect("Effect_Magic");
            
            
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "skill")) return true;

            return !Animator.GetBool(Skill);
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }
}