using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class OverloadAnimator : ReactionAnimator
    {

        public OverloadAnimator(Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Overload;
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