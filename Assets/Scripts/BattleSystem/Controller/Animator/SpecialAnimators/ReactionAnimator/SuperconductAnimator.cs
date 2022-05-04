using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SuperconductAnimator : ReactionAnimator
    {

        public SuperconductAnimator(Unit _unit) : base(_unit)
        {
            ReactionEnum = ElementReactionEnum.Superconduct;
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