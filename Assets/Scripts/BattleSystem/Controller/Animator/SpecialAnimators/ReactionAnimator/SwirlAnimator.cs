using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SwrilAnimator : ReactionAnimator
    {

        public SwrilAnimator(Unit _unit) : base(_unit)
        {
            ReactionEnum = ElementReactionEnum.Swirl;
        }

        public override void SpecialAct()
        {
            // TODO: ¥˝Prefab µœ÷
            // base.SpecialAct();
        }

        public override bool IsAnimationFinished()
        {
            return true;
        }

        public override void ShutDownAct()
        {
            // TODO: 
        }
    }
}