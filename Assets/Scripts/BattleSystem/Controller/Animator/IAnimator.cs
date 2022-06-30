using BattleSystem.Controller.Unit.UnitView;
using UnityEngine;

namespace BattleSystem.Controller.Animator
{
    /// <summary>
    /// 标识动画事件
    /// </summary>
    public interface IAnimator
    {
        public AnimatorType.AnimatorTypeEnum GetAnimatorType();
    }

    public interface ISourceAnimator : IAnimator
    {
        /// <summary>
        /// 获取动作
        /// </summary>
        public AnimatorType.SourceAnimator GetSourceAnimator();

        public void SourceAct();
        public bool IsAnimationFinished();

        public void ShutDownAct();

    }

    public interface ITargetAnimator : IAnimator
    {
        /// <summary>
        /// 获取更新后Unit界面
        /// </summary>
        /// <returns></returns>
        public UnitView GetFreshUnitView();

        /// <summary>
        /// 获取动作
        /// </summary>
        public AnimatorType.TargetAnimator GetTargetAnimator();

        public void TargetAct();

        public bool IsAnimationFinished();

        public void ShutDownAct();

    }

    public interface ISpecialAnimator : IAnimator
    {
        /// <summary>
        /// 获取特效生成位置
        /// </summary>
        public Transform GetTargetTransform();

        /// <summary>
        /// 获取特效对象（待定
        /// </summary>
        // public GameObject GetSpecialPrefabs();

        public void SpecialAct();

        public bool IsAnimationFinished();

        public void ShutDownAct();

        /// <summary>
        /// 获取特性（播放位置）
        /// </summary>
        public AnimatorType.AnimatorTypeEnum GetFeature();
    }

}