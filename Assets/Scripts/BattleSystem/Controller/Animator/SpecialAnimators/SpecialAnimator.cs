using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SpecialAnimator : ISpecialAnimator
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
        public readonly AnimatorType.SpecialAnimator SpecialAnimatorType;

        public AnimatorType.AnimatorTypeEnum FeatureTypeEnum;

        public Animator Animator;

        public SpecialAnimator(Unit unit, AnimatorType.SpecialAnimator specialAnimatorType)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            UnitView = unit.GetView();
            SpecialAnimatorType = specialAnimatorType;
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public SpecialAnimator(Unit unit)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            UnitView = unit.GetView();
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.SpecialAnimator;
        }

        public UnitView GetFreshUnitView()
        {
            return UnitView;
        }

        public AnimatorType.SpecialAnimator GetSpecialAnimator()
        {
            return SpecialAnimatorType;
        }

        public Transform GetTargetTransform()
        {
            return UnitEntity.transform;
        }

        public virtual void SpecialAct()
        {

        }

        public virtual bool IsAnimationFinished()
        {
            return true;
        }

        public virtual void ShutDownAct()
        {

        }

        public AnimatorType.AnimatorTypeEnum GetFeature()
        {
            return FeatureTypeEnum;
        }
    }
}