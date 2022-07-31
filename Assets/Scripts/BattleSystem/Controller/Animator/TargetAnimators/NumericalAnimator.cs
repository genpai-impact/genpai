using BattleSystem.Controller.EntityManager;
using BattleSystem.Service.Common;
using BattleSystem.Service.Effect;
using Utils;
using UnityEngine;
using System.Collections;

namespace BattleSystem.Controller.Animator.TargetAnimators
{
    /// <summary>
    /// 受击及播放伤害数字
    /// </summary>
    public class HittenAnimator : TargetAnimator
    {
      
        public readonly Damage Damage;
        private static readonly int Injured = UnityEngine.Animator.StringToHash("injured");
        private int _index = 200;

        public HittenAnimator(Service.Unit.Unit unit, Damage damage) : base(unit, AnimatorType.TargetAnimator.Hitten)
        {
            Damage = damage;
        }

        private IEnumerator AfterInjured()
        {
            //Debug.LogWarning("back" + _index);
            while (!Animator.GetCurrentAnimatorStateInfo(0).IsName("injured"))
            {
                //Debug.LogWarning("协程ing");
                yield return null;
            }
            while (Animator.GetCurrentAnimatorStateInfo(0).IsName("injured"))
            {
                //Debug.LogWarning("协程ing");
                yield return null;
            }
            //Debug.LogWarning("关闭injure: " + Animator.name);
            _isacting = false;
            AnimationHandle.Instance.DeleteAnimator("injured", Animator);
        }

        public override void TargetAct()
        {
            if (IsTriggerExist(Animator, "injured"))
            {
                _isacting = true; 
                AnimationHandle.Instance.AddAnimator("injured", Animator);
                Animator.SetTrigger(Injured);
                //Debug.LogWarning("front" + ++_index);
                //_mono = GameObject.FindObjectOfType<MonoBehaviour>();
                _mono.StartCoroutine(AfterInjured());

            }
            // TODO: 新建相关标识符
            // AudioManager.Instance.PlayerEffect("Effect.Reduce");

            AfterAct();
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "injured")) return true;
            return !_isacting;
            //return !Animator.GetBool(Injured);
        }

        public void AfterAct()
        {
            HittenNumManager.Instance.PlayDamage(Damage);

            UnitEntity.unitDisplay.Display(GetFreshUnitView());
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

        public CureAnimator(Service.Unit.Unit unit, Cure cure) : base(unit, AnimatorType.TargetAnimator.Cure)
        {
            Cure = cure;
        }

        public override void TargetAct()
        {
            // HittenNumManager.Instance.PlayDamage(damage);
            UnitEntity.unitDisplay.Display(GetFreshUnitView());
            
            AudioManager.Instance.PlayerEffect("Effect_Cure");
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