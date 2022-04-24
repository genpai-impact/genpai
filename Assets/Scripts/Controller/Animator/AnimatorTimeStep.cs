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
        /// </summary>
        public List<IAnimator> Sources;


        /// <summary>
        /// 用于标识当前时间步内动画作用者
        /// 主要视时间步类型主要效果为受击/更新UI
        /// </summary>
        public List<IAnimator> Targets;
    }

}