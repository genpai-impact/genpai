using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SpecialAnimator : ISpecialAnimator
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
        public AnimatorType.SpecialAnimator specialAnimatorType;

        public AnimatorType.AnimatorTypeEnum featureTypeEnum;

        public Animator specialAnimator;

        public SpecialAnimator(Unit _unit, AnimatorType.SpecialAnimator _specialAnimatorType)
        {
            unitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            unitView = _unit.GetView();
            specialAnimatorType = _specialAnimatorType;
            specialAnimator = unitEntity.UnitModelDisplay.animator;
        }

        public SpecialAnimator(Unit _unit)
        {
            unitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            unitView = _unit.GetView();
            specialAnimator = unitEntity.UnitModelDisplay.animator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.SpecialAnimator;
        }

        public UnitView GetFreshUnitView()
        {
            return unitView;
        }

        public AnimatorType.SpecialAnimator GetSpecialAnimator()
        {
            return specialAnimatorType;
        }

        public Transform GetTargetTransform()
        {
            return unitEntity.transform;
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
            return featureTypeEnum;
        }
    }
}