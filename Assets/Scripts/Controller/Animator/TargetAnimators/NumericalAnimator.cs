using UnityEditor;
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
    }
}