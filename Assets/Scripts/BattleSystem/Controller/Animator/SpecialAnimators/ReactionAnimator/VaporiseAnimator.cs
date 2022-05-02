using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class VaporiseAnimator : ReactionAnimator
    {
        public VaporiseAnimator(Unit _unit, AnimatorType.SpecialAnimator _specialAnimatorType) : base(_unit, _specialAnimatorType)
        {
            reactionName = "Vaporise";
        }

        public VaporiseAnimator(Unit _unit) : base(_unit)
        {
            reactionName = "Vaporise";
        }

        public override void SpecialAct()
        {
            base.SpecialAct();
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