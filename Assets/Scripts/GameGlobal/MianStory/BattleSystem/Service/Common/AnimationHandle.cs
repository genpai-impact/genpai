using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace BattleSystem.Service.Common
{
    public class AnimationHandle : Singleton<AnimationHandle>
    {
        //private readonly Dictionary<AnimationHandleSetEntity,bool> _animatorDictionary = new Dictionary<AnimationHandleSetEntity, bool>();//测试不方便，暂时去掉2022/7/28
        //private bool _onAnimation = true;
        private readonly Dictionary<string, bool> _animatorDictionary = new Dictionary<string, bool>();

        public void AddAnimator(string trigger, Animator animator)
        {
            if (trigger == "atk")
            {
                trigger = "attack";
            }
            //AnimationHandleSetEntity animationHandleSetEntity = new AnimationHandleSetEntity(animator, trigger);
            string key = animator.ToString()+" "+trigger;
            if (!_animatorDictionary.ContainsKey(key))
            {
                _animatorDictionary.Add(key, true);
            }
            else { 
                _animatorDictionary[key] = true;
            }
        }


        public void DeleteAnimator(string trigger, Animator animator)
        {
            if (trigger == "atk")
            {
                trigger = "attack";
            }
            //AnimationHandleSetEntity animationHandleSetEntity = new AnimationHandleSetEntity(animator, trigger);
            string key = animator.ToString() + " " + trigger;
            if (_animatorDictionary.ContainsKey(key))
            {
                _animatorDictionary[key] = false;
            }
            else {
                Debug.LogWarning("cant find!!!");
            }
        }
        /*public void SetGlobalAnimat() {
            _onAnimation |= true;
        }*/


        public bool AllAnimationOver() {
            bool ret=false;
            foreach (var v in _animatorDictionary) {
                ret &= v.Value;
                if(ret) 
                    break;
            }
            return !ret;
        }

/*        public bool AllAnimationOver()
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
        }*/

        private class AnimationHandleSetEntity
        {
            public readonly Animator Animator;
            public readonly string Trigger;
            int _index=0;

            private int getIndex() { 
                return _index++;
                if (_index > 100) {
                    _index = 0;
                }
            }
            public AnimationHandleSetEntity(Animator animator, string trigger)
            {
                getIndex();
                Animator = animator;
                Trigger = trigger;
            }

            public override bool Equals(object obj)
            {
                return obj is AnimationHandleSetEntity entity &&
                       EqualityComparer<Animator>.Default.Equals(Animator, entity.Animator) &&
                       Trigger == entity.Trigger && _index == entity._index;
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
