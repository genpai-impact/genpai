using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class MeltAnimator : ReactionAnimator
    {

        public MeltAnimator(Unit _unit) : base(_unit)
        {
            ReactionEnum = ElementReactionEnum.Melt;
            featureTypeEnum = AnimatorType.AnimatorTypeEnum.TargetAnimator;
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