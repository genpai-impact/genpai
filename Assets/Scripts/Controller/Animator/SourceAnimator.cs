using UnityEditor;
using UnityEngine;

namespace Genpai
{
    public class SourceAnimator : ISourceAnimator
    {
        /// <summary>
        /// 动画对象
        /// </summary>
        public UnitEntity unit;

        /// <summary>
        /// 待播放动画类型
        /// </summary>
        public AnimatorType.SourceAnimator sourceAnimator;

        public SourceAnimator(Unit _unit, AnimatorType.SourceAnimator _sourceAnimator)
        {
            unit = BucketEntityManager.Instance.GetUnitEntityByUnit(_unit);
            sourceAnimator = _sourceAnimator;
        }

        public AnimatorType.AnimatorTypeEnum GetAnimatorType()
        {
            return AnimatorType.AnimatorTypeEnum.SourceAnimator;
        }

        public AnimatorType.SourceAnimator GetSourceAnimator()
        {
            return sourceAnimator;
        }
    }
}