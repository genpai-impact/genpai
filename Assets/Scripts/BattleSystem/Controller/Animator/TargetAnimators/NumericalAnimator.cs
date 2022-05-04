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
        public HittenAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator, Damage _damage) : base(_unit, _targetAnimator)
        {
            damage = _damage;
        }

        public HittenAnimator(Unit _unit, Damage _damage) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.Hitten;
            damage = _damage;
        }

        public override void TargetAct()
        {
            if (isTriggerExist(targetAnimator, "injured"))
            {
                AnimationHandle.Instance.AddAnimator("injured", targetAnimator);
                targetAnimator.SetTrigger("injured");
            }

            HittenNumManager.Instance.PlayDamage(damage);
            if (unitEntity.GetUnit() == null)
            {
                UnitView unitView = unitEntity.UnitDisplay.unitView;
                unitView.HP = 0;
                unitEntity.UnitDisplay.FreshUnitUI(unitView);
            }
            else
                unitEntity.UnitDisplay.FreshUnitUI(unitView);
        }

        public override bool IsAnimationFinished()
        {
            if (!isTriggerExist(targetAnimator, "injured")) return true;

            return !targetAnimator.GetBool("injured");
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

        public CureAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator, Cure _cure) : base(_unit, _targetAnimator)
        {
            cure = _cure;
        }

        public CureAnimator(Unit _unit, Cure _cure) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.Cure;
            cure = _cure;
        }

        public override void TargetAct()
        {
            // HittenNumManager.Instance.PlayDamage(damage);
            unitEntity.UnitDisplay.FreshUnitUI(unitView);
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