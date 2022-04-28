using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class AttackAnimator : SourceAnimator
    {
        public AttackAnimator(Unit _unit, AnimatorType.SourceAnimator _sourceAnimatorType) : base(_unit, _sourceAnimatorType)
        {
        }

        public AttackAnimator(Unit _unit) : base(_unit)
        {
            sourceAnimatorType = AnimatorType.SourceAnimator.Attack;
        }

        public override void SourceAct()
        {
            if(isTriggerExist(sourceAnimator, "atk"))
            {
                AnimationHandle.Instance.AddAnimator("atk", sourceAnimator);
                sourceAnimator.SetTrigger("atk");
            }
        }

        public override bool IsAnimationFinished()
        {
            if(!isTriggerExist(sourceAnimator, "atk")) return true;

            return !sourceAnimator.GetBool("atk");
        }
    }
}