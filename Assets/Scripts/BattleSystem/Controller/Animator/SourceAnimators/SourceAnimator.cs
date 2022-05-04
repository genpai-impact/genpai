using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SourceAnimator : ISourceAnimator
    {
        /// <summary>
        /// 动画对象
        /// </summary>
        public UnitEntity unitEntity;

        /// <summary>
        /// 待播放动画类型
        /// </summary>
        public AnimatorType.SourceAnimator sourceAnimatorType;

        /// <summary>
        /// 待播动画器
        /// 虽然可以通过unit获得，但是我不喜欢这样
        /// </summary>
        public Animator sourceAnimator;

        public SourceAnimator(Unit _unit, AnimatorType.SourceAnimator _sourceAnimatorType)
        {
            unitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            sourceAnimatorType = _sourceAnimatorType;
            sourceAnimator = unitEntity.UnitModelDisplay.animator;
        }

        public SourceAnimator(Unit _unit)
        {
            unitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            sourceAnimator = unitEntity.UnitModelDisplay.animator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.SourceAnimator;
        }

        public AnimatorType.SourceAnimator GetSourceAnimator()
        {
            return sourceAnimatorType;
        }

        public virtual void SourceAct()
        {    
        }

        protected bool isTriggerExist(Animator animator, string str)
        {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                if (parameter.name == str) return true;
            }
            return false;
        }

        public virtual bool IsAnimationFinished()
        {
            return true;
        }

        public virtual void ShutDownAct()
        {
        }
    }
}