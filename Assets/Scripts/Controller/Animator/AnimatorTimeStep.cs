﻿using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Genpai
{

    /// <summary>
    /// 用于描述当前时间步内将执行动画
    /// </summary>
    public class AnimatorTimeStep
    {
        /// <summary>
        /// 用于标识当前时间步内动画触发者
        /// 仅可能存在**零或一个**触发者
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
            SetSourceAnimator(source);

            Targets = new List<ITargetAnimator>();
            AddTargetAnimator(targets);

            Specials = new List<ISpecialAnimator>();
            AddSpecialAnimator(specials);

        }


        public AnimatorTimeStep(ISourceAnimator source, ITargetAnimator target, List<ISpecialAnimator> specials = null)
        {
            SetSourceAnimator(source);

            Targets = new List<ITargetAnimator>();
            AddTargetAnimator(target);

            Specials = new List<ISpecialAnimator>();
            AddSpecialAnimator(specials);
        }

        public void SetSourceAnimator(ISourceAnimator source)
        {
            Sources = source;
        }

        public void AddTargetAnimator(List<ITargetAnimator> targets)
        {
            if (targets == null)
            {
                return;
            }
            Targets.AddRange(targets);
        }

        public void AddTargetAnimator(ITargetAnimator target)
        {
            if (target == null)
            {
                return;
            }
            Targets.Add(target);
        }

        public void AddSpecialAnimator(List<ISpecialAnimator> specials)
        {
            if (specials == null)
            {
                return;
            }
            Specials.AddRange(specials);
        }

        public void AddSpecialAnimator(ISpecialAnimator special)
        {
            if (special == null)
            {
                return;
            }
            Specials.Add(special);
        }
    }

}