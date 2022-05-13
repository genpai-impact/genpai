using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 受击及播放伤害数字
    /// </summary>
    public class HittenAnimator : TargetAnimator
    {
        public readonly Damage Damage;
        private static readonly int Injured = Animator.StringToHash("injured");

        public HittenAnimator(Unit unit, Damage damage) : base(unit, AnimatorType.TargetAnimator.Hitten)
        {
            Damage = damage;
        }

        public override void TargetAct()
        {
            if (IsTriggerExist(Animator, "injured"))
            {
                AnimationHandle.Instance.AddAnimator("injured", Animator);
                Animator.SetTrigger(Injured);
            }
            AudioManager.Instance.PlayerEffect("Effect.Reduce");

            AfterAct();
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "injured")) return true;

            return !Animator.GetBool(Injured);
        }

        public void AfterAct()
        {
            HittenNumManager.Instance.PlayDamage(Damage);

            UnitEntity.unitDisplay.FreshUnitUI(GetFreshUnitView());
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }

    /// <summary>
    /// 播放恢复数字
    /// </summary>
    public class CureAnimator : TargetAnimator
    {
        public Cure Cure;

        public CureAnimator(Unit unit, Cure cure) : base(unit, AnimatorType.TargetAnimator.Cure)
        {
            Cure = cure;
        }

        public override void TargetAct()
        {
            // HittenNumManager.Instance.PlayDamage(damage);
            UnitEntity.unitDisplay.FreshUnitUI(GetFreshUnitView());
            AudioManager.Instance.PlayerEffect("Effect.Cure");
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