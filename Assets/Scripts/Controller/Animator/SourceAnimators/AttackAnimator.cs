using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class AttackAnimator : SourceAnimator
    {
        private Damage attackDamage;

        private Vector3 sourceVector;

        private Vector3 targetVector;

        private GameObject attackObject;

        private BattleSite attackBattleSite;

        public AttackAnimator(Unit _unit, AnimatorType.SourceAnimator _sourceAnimatorType, Damage damage) : base(_unit, _sourceAnimatorType)
        {
            attackDamage = damage;

            sourceVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetSource().carrier.serial).transform.position;
            targetVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetTarget().carrier.serial).transform.position;

            attackObject = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetSource().carrier.serial);
            attackBattleSite = attackDamage.GetSource().carrier.ownerSite;
        }

        public AttackAnimator(Unit _unit, Damage damage) : base(_unit)
        {
            attackDamage = damage;
            sourceAnimatorType = AnimatorType.SourceAnimator.Attack;

            sourceVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetSource().carrier.serial).transform.position;
            targetVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetTarget().carrier.serial).transform.position;
        
            attackObject = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetSource().carrier.serial);
            attackBattleSite = attackDamage.GetSource().carrier.ownerSite;
        }

        public override void SourceAct()
        {
            if(isTriggerExist(sourceAnimator, "atk"))
            {
                AnimationHandle.Instance.AddAnimator("atk", sourceAnimator);

                attackObject.transform.position = targetVector;
                attackObject.transform.Translate((sourceVector-targetVector).normalized*4);

                sourceAnimator.SetTrigger("atk");
            }
        }

        public override bool IsAnimationFinished()
        {
            if(!isTriggerExist(sourceAnimator, "atk")) return true;

            return !sourceAnimator.GetBool("atk");
        }

        public override void FinishSourceAct()
        {
            attackObject.transform.position = sourceVector;
        }
    }
}