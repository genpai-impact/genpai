using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SourceAnimator : ISourceAnimator
    {
        /// <summary>
        /// 动画对象
        /// </summary>
        public readonly UnitEntity UnitEntity;

        /// <summary>
        /// 待播放动画类型
        /// </summary>
        public readonly AnimatorType.SourceAnimator SourceAnimatorType;

        /// <summary>
        /// 待播动画器
        /// 虽然可以通过unit获得，但是我不喜欢这样
        /// </summary>
        public readonly Animator Animator;

        public SourceAnimator(Unit unit, AnimatorType.SourceAnimator sourceAnimatorType)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            SourceAnimatorType = sourceAnimatorType;
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public SourceAnimator(Unit unit)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.SourceAnimator;
        }

        public AnimatorType.SourceAnimator GetSourceAnimator()
        {
            return SourceAnimatorType;
        }

        public virtual void SourceAct()
        {    
        }

        protected static bool IsTriggerExist(Animator animator, string str)
        {
            return animator != null && animator.parameters.Any(parameter => parameter.name == str);
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