using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class MeltAnimator : ReactionAnimator
    {
        public MeltAnimator(Unit _unit, AnimatorType.SpecialAnimator _specialAnimatorType) : base(_unit, _specialAnimatorType)
        {
            reactionName = "Melt";
        }

        public MeltAnimator(Unit _unit) : base(_unit)
        {
            reactionName = "Melt";
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