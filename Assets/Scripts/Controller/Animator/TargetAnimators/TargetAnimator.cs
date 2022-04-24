using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class TargetAnimator : ITargetAnimator
    {
        /// <summary>
        /// 动画对象
        /// </summary>
        public UnitEntity unit;

        /// <summary>
        /// 更新数据
        /// </summary>
        public UnitView unitView;

        /// <summary>
        /// 待播放动画类型
        /// </summary>
        public AnimatorType.TargetAnimator targetAnimator;

        public TargetAnimator(Unit _unit, AnimatorType.TargetAnimator _targetAnimator)
        {
            unit = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            unitView = _unit.GetView();
            targetAnimator = _targetAnimator;
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
            return targetAnimator;
        }
    }
}