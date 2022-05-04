using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class VaporiseAnimator : ReactionAnimator
    {

        public VaporiseAnimator(Unit _unit) : base(_unit)
        {
            ReactionEnum = ElementReactionEnum.Vaporise;
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