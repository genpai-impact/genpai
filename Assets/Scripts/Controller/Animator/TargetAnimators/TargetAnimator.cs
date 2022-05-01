using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class TargetAnimator : ITargetAnimator
    {
        /// <summary>
        /// 动画对象
        /// </summary>
        public UnitEntity unitEntity;

        /// <summary>
        /// 更新数据
        /// </summary>
        public UnitView unitView;

        /// <summary>
        /// 待播放动画类型
        /// </summary>
        public AnimatorType.TargetAnimator targetAnimatorType;

        public Animator targetAnimator;


        public TargetAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimatorType)
        {
            unitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            unitView = _unit.GetView();
            targetAnimatorType = _targetAnimatorType;
            targetAnimator = unitEntity.UnitModelDisplay.animator;
        }

        public TargetAnimator(Unit _unit)
        {
            unitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            unitView = _unit.GetView();
            targetAnimator = unitEntity.UnitModelDisplay.animator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.TargetAnimator;
        }

        public UnitView GetFreshUnitView()
        {
            return unitView;
        }

        public AnimatorType.TargetAnimator GetTargetAnimator()
        {
            return targetAnimatorType;
        }

        public virtual void TargetAct()
        {
        }
        protected bool isTriggerExist(Animator animator, string str)
        {
            if(animator==null) return false;
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

    }
}