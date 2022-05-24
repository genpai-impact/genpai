using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class AttackAnimator : SourceAnimator
    {
        private readonly Vector3 _sourceVector;

        private readonly Vector3 _targetVector;

        private readonly GameObject _attackObject;

        private BattleSite _attackBattleSite;
        private static readonly int Atk = Animator.StringToHash("atk");

        public AttackAnimator(Unit unit, Damage damage) : base(unit, AnimatorType.SourceAnimator.Attack)
        {
            _sourceVector = BucketEntityManager.Instance.GetBucketBySerial(damage.GetSource().Carrier.serial).transform.position;
            _targetVector = BucketEntityManager.Instance.GetBucketBySerial(damage.GetTarget().Carrier.serial).transform.position;

            _attackObject = UnitEntity.carrier.gameObject;

            _attackBattleSite = damage.GetSource().Carrier.ownerSite;
        }

        public override void SourceAct()
        {
            if (IsTriggerExist(Animator, "atk"))
            {
                AnimationHandle.Instance.AddAnimator("atk", Animator);

                _attackObject.transform.position = _targetVector;
                _attackObject.transform.Translate((_sourceVector - _targetVector).normalized * 4);

                Animator.SetTrigger(Atk);

                // AudioManager.Instance.PlayerEffect("Play_bells_2");
                AudioManager.Instance.PlayerEffect("Effect_Attack_Smash");
            }
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "atk")) return true;

            return !Animator.GetBool(Atk);
        }

        public override void ShutDownAct()
        {
            _attackObject.transform.position = _sourceVector;
        }
    }
}