using System.Linq;
using BattleSystem.Controller.Bucket;
using BattleSystem.Controller.Unit;
using BattleSystem.Service.Process;
using UnityEngine;

namespace BattleSystem.Controller.Animator.SourceAnimators
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
        public readonly UnityEngine.Animator Animator;

        protected bool _isacting = false;

        protected MonoBehaviour _mono = NormalProcessManager.Instance; //= GameObject.FindObjectOfType<MonoBehaviour>();
        //protected MonoBehaviour[] _allmono = GameObject.FindObjectsOfType<MonoBehaviour>();

        public SourceAnimator(Service.Unit.Unit unit, AnimatorType.SourceAnimator sourceAnimatorType)
        {
            UnitEntity = BucketEntityManager.Instance.GetUnitEntityByUnit(unit);
            SourceAnimatorType = sourceAnimatorType;
            Animator = UnitEntity.unitModelDisplay.animator;
        }

        public SourceAnimator(Service.Unit.Unit unit)
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