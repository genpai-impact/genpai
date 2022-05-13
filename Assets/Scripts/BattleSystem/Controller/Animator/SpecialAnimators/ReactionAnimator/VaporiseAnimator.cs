using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class VaporiseAnimator : ReactionAnimator
    {

        public VaporiseAnimator(Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Vaporise;
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