using BattleSystem.Service.Common;
using Utils;
using System.Collections;
using UnityEngine;

namespace BattleSystem.Controller.Animator.SourceAnimators
{
    public class SpellAnimator : SourceAnimator
    {
        private static readonly int Skill = UnityEngine.Animator.StringToHash("skill");
        public SpellAnimator(Service.Unit.Unit unit) : base(unit, AnimatorType.SourceAnimator.Spell) { }
        private int _index = 100;
       
        public override void SourceAct()
        {
            if (IsTriggerExist(Animator, "skill"))
            {
                _isacting=true; 
                AnimationHandle.Instance.AddAnimator("skill", Animator);
                Animator.SetTrigger(Skill);
//                Debug.LogWarning("back" + _index);
                //_mono= GameObject.FindObjectOfType<MonoBehaviour>();
                _mono.StartCoroutine(finishAction());
                
            }
            
            AudioManager.Instance.PlayerEffect("Effect_Magic");
            
            
        }

        private IEnumerator finishAction()
        {
            //Debug.LogWarning("front" + ++_index);
            while (!Animator.GetCurrentAnimatorStateInfo(0).IsName("skill"))
            {
                //Debug.LogWarning("协程ing");
                yield return null;
            }
            while (Animator.GetCurrentAnimatorStateInfo(0).IsName("skill"))
            {
                //Debug.LogWarning("协程ing");
                yield return null;
            }
            //Debug.LogWarning("关闭skill: " + Animator.name);
            _isacting = false;
            AnimationHandle.Instance.DeleteAnimator("skill", Animator);
        }

        public override bool IsAnimationFinished()
        {
            if (!IsTriggerExist(Animator, "skill")) return true;

            return _isacting;
            //return !Animator.GetBool(Skill);
        }

        public override void ShutDownAct()
        {
            base.ShutDownAct();
        }
    }
}