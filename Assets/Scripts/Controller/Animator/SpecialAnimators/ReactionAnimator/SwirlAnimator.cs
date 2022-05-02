using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SwrilAnimator : ReactionAnimator
    {
        public SwrilAnimator(Unit _unit, AnimatorType.SpecialAnimator _specialAnimatorType) : base(_unit, _specialAnimatorType)
        {
            reactionName = "Swril";
        }

        public SwrilAnimator(Unit _unit) : base(_unit)
        {
            reactionName = "Swril";
        }

        public override void SpecialAct()
        {
        }

        public override bool IsAnimationFinished()
        {
            return true;
        }

        public override void ShutDownAct()
        {
        }
    }
}