using UnityEditor;
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
        public ISourceAnimator Source;


        /// <summary>
        /// 用于标识当前时间步内动画作用者
        /// 主要视时间步类型主要效果为受击/更新UI
        /// </summary>
        public List<ITargetAnimator> Targets;


        /// <summary>
        /// 用于在时间步内播放特效
        /// </summary>
        public List<ISpecialAnimator> Specials;

        private float acttime;

        public AnimatorTimeStep()
        {
            Source = null;
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
            Source = source;
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

        /// <summary>
        /// 这个方法有啥Bug？
        /// </summary>
        public void LogTimeStepInfo()
        {
            string ret = "这是一个AnimatorTimeStep";

            if (Source != null)
            {
                // 由***触发
                string source = "由" + (Source as SourceAnimator).unitEntity.UnitDisplay.unitView.unitName + "触发";
                ret += source;
            }

            // 影响了***
            string target = "影响了" + (Targets[0] as TargetAnimator).unitEntity.UnitDisplay.unitView.unitName + "等单位";
            ret += target;
            Debug.Log(ret);

        }

        public void ActSourceAnimator()
        {
            acttime = Time.time;
            if(Source!=null)
                Source.SourceAct();
        }

        public bool isSourceAnimationRunning()
        {
            if(Source!=null)
                return !Source.IsAnimationFinished();
            else return false;
        }

        public void ActTargetAnimator()
        {
            foreach(ITargetAnimator targetAnimator in Targets)
            {
                targetAnimator.TargetAct();
            }
        }

        public bool isTargetAnimationRunning()
        {
            foreach(ITargetAnimator targetAnimator in Targets)
            {
                if(!targetAnimator.IsAnimationFinished() || Time.time-acttime<2.0f)
                    return true;
            }

            return false;
        }

        public void ActSpecialAnimator()
        {
            foreach(ISpecialAnimator specialAnimator in Specials)
            {
                specialAnimator.SpecialAct();
            }
        }

        public bool isSpecialAnimationRunning()
        {
            foreach(ISpecialAnimator specialAnimator in Specials)
            {
                if(!specialAnimator.IsAnimationFinished())
                    return true;
            }

            return false;
        }

    }

}