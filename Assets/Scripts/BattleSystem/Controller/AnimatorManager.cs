using System.Collections.Generic;
using BattleSystem.Controller.Animator;
using Utils;

namespace BattleSystem.Controller
{
    /// <summary>
    /// 凹凸曼  2022/4/16
    /// 动画管理器，攻击动画，受击动画，ui更新，元素特效，死亡等动画的调用都放在这里，实现攻击动画的按序播放
    /// 仍待改进：
    /// 实际效果一般，只能说如果能耐心地等待每次动画运行结束再操作可能能勉强达到按序的要求
    /// 同一个目标多个动画请求到达时会有延时，似乎是动画本身的问题。比如刻晴芭芭拉攻击有个起手时间（意义不明的idle状态），导致播放时会有问题
    /// 多个对象同时发出攻击指令时可能会导致一些动画效果结算顺序出问题
    /// 动画播放时玩家依然可以操作，导致最后效果看起来更乱
    /// 写的丑，以后一定自学设计模式
    /// 个人想法:
    /// 这部分代码其实只是相当于一个转接口，很多东西依然是其他类的功能，使得这部分代码写起来很难受
    /// 动画管理器的实现还是想再讨论一下，战斗过程涉及到的图案更新还挺多挺乱的，希望讨论下实现的大概框架
    /// </summary>
    public class AnimatorManager : MonoSingleton<AnimatorManager>
    {
        private readonly Queue<AnimatorTimeStep> _animatorTimeStepsQueue = new Queue<AnimatorTimeStep>();

        private AnimatorTimeStep _animatorTimeStepOnDisplay;

        private enum AnimatorTimeStepStage
        {
            Idle,
            Source,
            Target
        }

        private AnimatorTimeStepStage _animatorTimeStepStage;

        // 清除上一轮中未播放完的动画
        // 其实影响应该不大
        public void Fresh()
        {
            _animatorTimeStepsQueue.Clear();
        }

        /// <summary>
        /// 将要播的动画输入queue
        /// 使用AnimatorTimeStep管理一轮动画流程
        /// 这个挺合理的，理想状态是把所有实际动画播放都放进AnimatorTimeStep
        /// 但是
        /// 1、为了实现去耦合我觉得还是要写多态
        /// 2、如何在实现每一次行为足够合理，虽说如果能按单位分开，感觉应该也可以
        /// 3、AnimatorTimeStep是否完整，这个就再说吧。（最大的问题是summon应该不是在effectManager做的，所以可能就要 搞事情:)
        /// </summary>
        public void InsertAnimatorTimeStep(AnimatorTimeStep animatorTimeStep)
        {
            _animatorTimeStepsQueue.Enqueue(animatorTimeStep);
        }

        public void InsertAnimatorTimeStep(Queue<AnimatorTimeStep> animatorTimeStepQueue)
        {
            foreach (var animatorTimeStep in animatorTimeStepQueue)
                _animatorTimeStepsQueue.Enqueue(animatorTimeStep);
        }

        /// <summary>
        /// 按顺序播放动画，以一次攻击以及其结算结束过程中触发的动画为一个流程
        /// 工作流程：
        /// 1.检测是否有攻击或受击在播放，若无，查看是否有攻击请求，有则继续进行，无则结束
        /// 2.设置攻击动画
        /// 3.按照trigger取出动画请求，直到遇到下一个攻击请求。执行受击动画，并将buff、reaction、fall的显示请求放入队列管理
        /// 4.等待受击动画结束，显示伤害、元素反应特效、角色死亡
        /// 5.一次流程结束
        /// 待优化：
        /// 具体实现效果（希望讨论一下
        /// 代码结构（等我看一下设计模式
        /// </summary>
        private void FixedUpdate()
        {
            switch (_animatorTimeStepStage)
            {
                case AnimatorTimeStepStage.Idle:
                    if (_animatorTimeStepsQueue.Count > 0)
                    {
                        // 取TimeStep
                        _animatorTimeStepOnDisplay = _animatorTimeStepsQueue.Peek();
                        _animatorTimeStepsQueue.Dequeue();
                        // 播TimeStep
                        _animatorTimeStepStage = AnimatorTimeStepStage.Source;

                        _animatorTimeStepOnDisplay.ActSourceAnimator();
                        _animatorTimeStepOnDisplay.ActSpecialAnimator(AnimatorType.AnimatorTypeEnum.SourceAnimator);
                    }
                    break;
                case AnimatorTimeStepStage.Source:
                    if (!_animatorTimeStepOnDisplay.IsSourceAnimationRunning()
                        && !_animatorTimeStepOnDisplay.IsSpecialAnimationRunning())
                    {
                        _animatorTimeStepStage = AnimatorTimeStepStage.Target;

                        _animatorTimeStepOnDisplay.ActTargetAnimator();
                        _animatorTimeStepOnDisplay.ActSpecialAnimator(AnimatorType.AnimatorTypeEnum.TargetAnimator);
                    }
                    break;
                case AnimatorTimeStepStage.Target:
                    if (!_animatorTimeStepOnDisplay.IsTargetAnimationRunning()
                        && !_animatorTimeStepOnDisplay.IsSpecialAnimationRunning())
                    {
                        _animatorTimeStepOnDisplay.FinishSourceAct();
                        _animatorTimeStepOnDisplay.FinishTargetAct();
                        _animatorTimeStepStage = AnimatorTimeStepStage.Idle;
                    }
                    break;
                default:
                    return;
            }
        }

    }
}