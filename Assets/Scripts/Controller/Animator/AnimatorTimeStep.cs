using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Genpai
{

    /// <summary>
    /// 用于描述当前时间步内将执行动画
    /// </summary>
    public class AnimatorTimeStep
    {
        /// <summary>
        /// 用于标识当前时间步内动画触发者
        /// 仅可能存在一个触发者
        /// </summary>
        public ISourceAnimator Sources;


        /// <summary>
        /// 用于标识当前时间步内动画作用者
        /// 主要视时间步类型主要效果为受击/更新UI
        /// </summary>
        public List<ITargetAnimator> Targets;


        /// <summary>
        /// 用于在时间步内播放特效
        /// </summary>
        public List<ISpecialAnimator> Specials;

        public AnimatorTimeStep()
        {
            Sources = null;
            Targets = new List<ITargetAnimator>();
            Specials = new List<ISpecialAnimator>();
        }

        public AnimatorTimeStep(ISourceAnimator source, List<ITargetAnimator> targets, List<ISpecialAnimator> specials = null)
        {
            Sources = source;
            Targets = targets;
            Specials = specials;

            if (Specials == null)
            {
                Specials = new List<ISpecialAnimator>();
            }
        }

        public AnimatorTimeStep(ISourceAnimator source, ITargetAnimator target, List<ISpecialAnimator> specials = null)
        {
            Sources = source;

            Targets = new List<ITargetAnimator>();
            Targets.Add(target);

            Specials = specials;

            if (Specials == null)
            {
                Specials = new List<ISpecialAnimator>();
            }
        }

        public void AddTargetAnimator(List<ITargetAnimator> targets)
        {
            Targets.AddRange(targets);
        }

        public void AddTargetAnimator(ITargetAnimator target)
        {
            Targets.Add(target);
        }
    }

}