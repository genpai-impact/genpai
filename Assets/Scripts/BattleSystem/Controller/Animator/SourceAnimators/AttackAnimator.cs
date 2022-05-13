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

            sourceVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetSource().Carrier.serial).transform.position;
            targetVector = BucketEntityManager.Instance.GetBucketBySerial(attackDamage.GetTarget().Carrier.serial).transform.position;

            attackObject = unitEntity.carrier.gameObject;

            attackBattleSite = attackDamage.GetSource().Carrier.ownerSite;
        }

        public override void SourceAct()
        {
            if (IsTriggerExist(Animator, "atk"))
            {
                AnimationHandle.Instance.AddAnimator("atk", Animator);

                attackObject.transform.position = targetVector;
                attackObject.transform.Translate((sourceVector - targetVector).normalized * 4);

                Animator.SetTrigger("atk");

                AudioManager.Instance.PlayerEffect("Play_bells_2");
            }
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "atk")) return true;

            return !Animator.GetBool("atk");
        }

        public override void ShutDownAct()
        {
            attackObject.transform.position = sourceVector;
        }
    }
}