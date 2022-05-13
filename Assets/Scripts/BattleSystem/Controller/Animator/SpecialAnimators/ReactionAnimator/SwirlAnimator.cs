using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SwrilAnimator : ReactionAnimator
    {

        public SwrilAnimator(Unit unit) : base(unit)
        {
            ReactionEnum = ElementReactionEnum.Swirl;
        }

        public override void SpecialAct()
        {
            // 待Prefab实现
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