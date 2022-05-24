using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class ElectroChargeAnimator : ReactionAnimator
    {

        public ElectroChargeAnimator(Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.ElectroCharge;
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