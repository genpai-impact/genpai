using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SpellAnimator : SourceAnimator
    {
        public SpellAnimator(Unit _unit) : base(_unit, AnimatorType.SourceAnimator.Spell) { }

        public override void SourceAct()
        {
            if (isTriggerExist(sourceAnimator, "skill"))
            {
                AnimationHandle.Instance.AddAnimator("skill", sourceAnimator);

                sourceAnimator.SetTrigger("skill");
            }
        }

        public override bool IsAnimationFinished()
        {
            if (!isTriggerExist(sourceAnimator, "skill")) return true;

            return !sourceAnimator.GetBool("skill");
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }
}