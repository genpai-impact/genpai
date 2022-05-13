using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SpellAnimator : SourceAnimator
    {
        public SpellAnimator(Unit _unit) : base(_unit, AnimatorType.SourceAnimator.Spell) { }

        public override void SourceAct()
        {
            if (IsTriggerExist(Animator, "skill"))
            {
                AnimationHandle.Instance.AddAnimator("skill", Animator);

                Animator.SetTrigger("skill");
            }
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "skill")) return true;

            return !Animator.GetBool("skill");
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }
}