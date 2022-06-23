using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace BattleSystem.Service.Common
{
    public class AnimationHandle : Singleton<AnimationHandle>
    {
        private readonly HashSet<AnimationHandleSetEntity> _animatorSet = new HashSet<AnimationHandleSetEntity>();

        public void AddAnimator(string trigger, Animator animator)
        {
            if (trigger == "atk")
            {
                trigger = "attack";
            }
            AnimationHandleSetEntity animationHandleSetEntity = new AnimationHandleSetEntity(animator, trigger);
            if (!_animatorSet.Contains(animationHandleSetEntity))
            {
                _animatorSet.Add(animationHandleSetEntity);
            }
        }

        public bool AllAnimationOver()
        {
            foreach (var animator in _animatorSet)
            {
                if (animator.Animator == null) continue;

                AnimatorClipInfo[] animatorClipInfos = animator.Animator.GetCurrentAnimatorClipInfo(0);
                if (animatorClipInfos.Length == 0)
                {
                    continue;
                }
                //Debug.Log(animatorClipInfos[0].clip.name + " animator.Trigger " + animator.Trigger);
                if (animatorClipInfos[0].clip.name == animator.Trigger)
                {
                    return false;
                }
                //Debug.Log(animatorStateInfo.IsName(animator.Trigger) + " animator.Trigger " + animator.Trigger);
                //break;
                //if (animatorStateInfo.IsName(animator.Trigger))
                //{
                //    Debug.Log(animator.Trigger + " isRunning");
                //    return false;
                //}
            }
            
            
            return true;
        }

        private class AnimationHandleSetEntity
        {
            public readonly Animator Animator;
            public readonly string Trigger;

            public AnimationHandleSetEntity(Animator animator, string trigger)
            {
                Animator = animator;
                Trigger = trigger;
            }

            public override bool Equals(object obj)
            {
                return obj is AnimationHandleSetEntity entity &&
                       EqualityComparer<Animator>.Default.Equals(Animator, entity.Animator) &&
                       Trigger == entity.Trigger;
            }

            public override int GetHashCode()
            {
                int hashCode = -761558944;
                hashCode = hashCode * -1521134295 + EqualityComparer<Animator>.Default.GetHashCode(Animator);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Trigger);
                return hashCode;
            }
        }
    }
}
