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
            AnimationHandle.Instance.AddAnimator("atk", sourceAnimator);
            if(isTriggerExist(sourceAnimator, "atk"))
                sourceAnimator.SetTrigger("atk");
        }
    }
}