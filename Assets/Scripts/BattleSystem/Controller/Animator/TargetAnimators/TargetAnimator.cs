using System.Linq;
using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.Unit;
using BattleSystem.Controller.Unit.UnitView;

namespace BattleSystem.Controller.Animator.TargetAnimators
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

        protected readonly UnityEngine.Animator Animator;


        public TargetAnimator(Service.Unit.Unit unit, AnimatorType.TargetAnimator targetAnimatorType)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            UnitView = unit.GetView();
            TargetAnimatorType = targetAnimatorType;
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public TargetAnimator(Service.Unit.Unit unit)
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
        protected static bool IsTriggerExist(UnityEngine.Animator animator, string str)
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