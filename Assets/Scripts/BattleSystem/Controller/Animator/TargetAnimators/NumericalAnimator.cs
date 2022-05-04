using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 受击及播放伤害数字
    /// </summary>
    public class HittenAnimator : TargetAnimator
    {
        public Damage damage;

        public HittenAnimator(Unit _unit, Damage _damage) : base(_unit, AnimatorType.TargetAnimator.Hitten)
        {
            damage = _damage;
        }

        public override void TargetAct()
        {
            if (isTriggerExist(targetAnimator, "injured"))
            {
                AnimationHandle.Instance.AddAnimator("injured", targetAnimator);
                targetAnimator.SetTrigger("injured");
            }

            AfterAct();
        }

        public override bool IsAnimationFinished()
        {
            if (!isTriggerExist(targetAnimator, "injured")) return true;

            return !targetAnimator.GetBool("injured");
        }

        public void AfterAct()
        {
            HittenNumManager.Instance.PlayDamage(damage);

            unitEntity.UnitDisplay.FreshUnitUI(GetFreshUnitView());
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
        public Cure cure;

        public CureAnimator(Unit _unit, Cure _cure) : base(_unit, AnimatorType.TargetAnimator.Cure)
        {
            cure = _cure;
        }

        public override void TargetAct()
        {
            // HittenNumManager.Instance.PlayDamage(damage);
            unitEntity.UnitDisplay.FreshUnitUI(GetFreshUnitView());
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