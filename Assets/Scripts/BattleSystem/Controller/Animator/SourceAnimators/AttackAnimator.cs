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

        public AttackAnimator(Unit _unit, Damage damage) : base(_unit, AnimatorType.SourceAnimator.Attack)
        {
            attackDamage = damage;

            sourceVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetSource().carrier.serial).transform.position;
            targetVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetTarget().carrier.serial).transform.position;

            attackObject = unitEntity.carrier.gameObject;

            attackBattleSite = attackDamage.GetSource().carrier.ownerSite;
        }

        public override void SourceAct()
        {
            if (isTriggerExist(sourceAnimator, "atk"))
            {
                AnimationHandle.Instance.AddAnimator("atk", sourceAnimator);

                attackObject.transform.position = targetVector;
                attackObject.transform.Translate((sourceVector - targetVector).normalized * 4);

                sourceAnimator.SetTrigger("atk");

                AudioManager.Instance.PlayerEffect();
            }
        }

        public override bool IsAnimationFinished()
        {
            if (!isTriggerExist(sourceAnimator, "atk")) return true;

            return !sourceAnimator.GetBool("atk");
        }

        public override void ShutDownAct()
        {
            attackObject.transform.position = sourceVector;
        }
    }
}