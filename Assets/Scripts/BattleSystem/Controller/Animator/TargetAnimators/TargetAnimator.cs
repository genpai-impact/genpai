using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class TargetAnimator : ITargetAnimator
    {
        /// <summary>
        /// 动画对象
        /// </summary>
        public readonly UnitEntity UnitEntity;

        /// <summary>
        /// 更新数据
        /// </summary>
        public readonly UnitView UnitView;

        /// <summary>
        /// 待播放动画类型
        /// </summary>
        public readonly AnimatorType.TargetAnimator TargetAnimatorType;

        protected readonly Animator Animator;


        public TargetAnimator(Unit unit, AnimatorType.TargetAnimator targetAnimatorType)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            UnitView = unit.GetView();
            TargetAnimatorType = targetAnimatorType;
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public TargetAnimator(Unit unit)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            UnitView = unit.GetView();
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.TargetAnimator;
        }

        public UnitView GetFreshUnitView()
        {
            return UnitView;
        }

        public AnimatorType.TargetAnimator GetTargetAnimator()
        {
            return TargetAnimatorType;
        }

        public virtual void TargetAct()
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