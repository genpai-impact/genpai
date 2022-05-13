using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SpellAnimator : SourceAnimator
    {
        private static readonly int Skill = Animator.StringToHash("skill");
        public SpellAnimator(Unit unit) : base(unit, AnimatorType.SourceAnimator.Spell) { }

        public override void SourceAct()
        {
            if (IsTriggerExist(Animator, "skill"))
            {
                AnimationHandle.Instance.AddAnimator("skill", Animator);

                Animator.SetTrigger(Skill);
            }
            
            
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