using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class MeltAnimator : ReactionAnimator
    {

        public MeltAnimator(Unit _unit) : base(_unit)
        {
            ReactionEnum = ElementReactionEnum.Melt;
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