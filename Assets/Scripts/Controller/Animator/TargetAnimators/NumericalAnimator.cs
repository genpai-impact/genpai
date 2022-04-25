﻿using UnityEditor;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 用于Hitten和Cure两种跟跳字相关的
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
            if(isTriggerExist(targetAnimator, "injured"))
                targetAnimator.SetTrigger("injured");
            HittenNumManager.Instance.PlayDamage(damage);
            unitEntity.UnitDisplay.FreshUnitUI(unitEntity.UnitDisplay.unitView);
        }
    }

    public class CureAnimator : TargetAnimator
    {
        public Damage damage;
        public CureAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator, Damage _damage) : base(_unit, _targetAnimator)
        {
            damage = _damage;
        }

        public CureAnimator(Unit _unit, Damage _damage) : base(_unit)
        {
            targetAnimatorType = AnimatorType.TargetAnimator.Cure;
            damage = _damage;
        }

        public override void TargetAct()
        {
            if(isTriggerExist(targetAnimator, "injured"))
                targetAnimator.SetTrigger("injured");
            HittenNumManager.Instance.PlayDamage(damage);
            unitEntity.UnitDisplay.FreshUnitUI(unitEntity.UnitDisplay.unitView);
        }
    }
}