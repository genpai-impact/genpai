using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class MeltAnimator : ReactionAnimator
    {

        public MeltAnimator(Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Melt;
            FeatureTypeEnum = AnimatorType.AnimatorTypeEnum.TargetAnimator;
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