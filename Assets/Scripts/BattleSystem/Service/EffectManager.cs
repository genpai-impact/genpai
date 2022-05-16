using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 统一效果管理器（伤害、恢复、Buff and so on）
    /// </summary>
    public class EffectManager : Singleton<EffectManager>
    {
        /// <summary>
        /// 当前正在处理效果时间序列
        /// </summary>
        private LinkedList<EffectTimeStep> _currentEffectList;

        /// <summary>
        /// 当前正在执行时间步
        /// </summary>
        private LinkedListNode<EffectTimeStep> _timeStepEffect;

        /// <summary>
        /// 待更新死亡清单
        /// </summary>
        private List<Unit> _fallList;

        /// <summary>
        /// 生成动画序列
        /// </summary>
        private Queue<AnimatorTimeStep> _animatorTimeSteps;

        /// <summary>
        /// 效果序列处理函数
        /// </summary>
        public void TakeEffect(LinkedList<EffectTimeStep> effectList)
        {
            _currentEffectList = effectList;
            ProcessEffect();
        }

        public void TakeEffect(EffectTimeStep effectList)
        {
            _currentEffectList = new LinkedList<EffectTimeStep>();
            _currentEffectList.AddLast(effectList);
            ProcessEffect();
        }

        /// <summary>
        /// 时间序列计算
        /// </summary>
        private void ProcessEffect()
        {
            // EffectList的结构为双层列表，第一层代表每个时间步，第二层代表单个时间步内执行同步操作
            _timeStepEffect = _currentEffectList.First;

            _fallList = new List<Unit>();
            _animatorTimeSteps = new Queue<AnimatorTimeStep>();

            while (_timeStepEffect != null)
            {
                // 执行时间
                DealTimeStep();
                // 创建动画
                AnimatorTimeStep animatorTimeStep = AnimatorGenerator.GenerateAnimatorByEffectTimeStep(_timeStepEffect.Value);
                _animatorTimeSteps.Enqueue(animatorTimeStep);


                _timeStepEffect = _timeStepEffect.Next;
            }

            AnimatorManager.Instance.InsertAnimatorTimeStep(_animatorTimeSteps);
            SetFall();
        }

        /// <summary>
        /// 执行当前时间步
        /// </summary>
        private void DealTimeStep()
        {


            HashSet<Damage> damageSet = new HashSet<Damage>();

            // 实现当前时间步内效果
            foreach (IEffect effect in _timeStepEffect.Value.EffectList)
            {
                // Debug.Log(effect.GetType().Name);

                switch (effect.GetType().Name)
                {
                    case "AddBuff":
                        ((AddBuff)effect).Add();
                        break;
                    case "DelBuff":
                        ((DelBuff)effect).Del();
                        break;
                    case "Damage":
                        DealDamage((Damage)effect, ref damageSet);
                        break;
                    case "ReactionDamage":
                        DealDamage((Damage)effect, ref damageSet);
                        break;
                    case "Cure":
                        ((Cure)effect).CureUnit();
                        break;
                    default:
                        break;
                }

            }
            // 更新伤害
            UnitTakeDamage(damageSet);
        }


        /// <summary>
        /// 实现伤害效果
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="damageSet"></param>
        private void DealDamage(Damage effect, ref HashSet<Damage> damageSet)
        {
            DamageCalculator.Instance.Calculate(ref effect);

            damageSet.Add(effect);
        }

        /// <summary>
        /// 在当前时间步后插入临时时间步
        /// 主要用于伤害计算器调用插入剧变反应AOE
        /// </summary>
        /// <param name="newTimeStepEffectList">下一时间步待执行效果</param>
        /// <param name="atLast"></param>
        public void InsertTimeStep(EffectTimeStep newTimeStepEffectList, bool atLast = false)
        {
            if (atLast)
            {
                _currentEffectList.AddLast(newTimeStepEffectList);
            }
            _currentEffectList.AddAfter(_timeStepEffect, newTimeStepEffectList);
        }

        /// <summary>
        /// 造成伤害及动画UI更新
        /// </summary>
        /// <param name="damageSet"></param>
        private void UnitTakeDamage(HashSet<Damage> damageSet)
        {
            // 结算当前时间步所有伤害
            foreach (Damage damage in damageSet)
            {
                if (damage.Target.IsFall)
                {
                    continue;
                }

                bool isFall = damage.ApplyDamage();


                // 判断死亡（流程结束统一实现动画）
                if (isFall)
                {
                    _fallList.Add(damage.GetTarget());
                }
                else
                {
                    // UI更新
                    if (damage.Target.UnitType == CardType.Chara)
                    {
                        // GameContext.Instance.GetPlayerBySite(damage.target.ownerSite).CharaManager.RefreshCharaUI(damage.target.GetView());
                    }
                }
            }
        }

        /// <summary>
        /// 设置死亡动画
        /// </summary>
        private void SetFall()
        {
            // 设置死亡
            foreach (Unit fallUnit in _fallList)
            {
                fallUnit.SetFall();
            }
            _animatorTimeSteps.Clear();
            _animatorTimeSteps.Enqueue(AnimatorGenerator.GenerateFallTimeStep(_fallList));
            AnimatorManager.Instance.InsertAnimatorTimeStep(_animatorTimeSteps);
        }

    }
}