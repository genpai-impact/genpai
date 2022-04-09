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
    public class NewEffectManager : Singleton<NewEffectManager>
    {
        /// <summary>
        /// 当前正在处理效果时间序列
        /// </summary>
        public LinkedList<List<INewEffect>> CurrentEffectList;

        /// <summary>
        /// 当前正在执行时间步
        /// </summary>
        public LinkedListNode<List<INewEffect>> TimeStepEffect;

        /// <summary>
        /// 待更新死亡清单
        /// </summary>
        public List<NewUnit> fallList;

        /// <summary>
        /// 效果序列处理函数
        /// </summary>
        /// <param name="EffectList">待处理效果序列列表</param>
        public void TakeEffect(LinkedList<List<INewEffect>> EffectList)
        {
            CurrentEffectList = EffectList;
            ProcessEffect();
        }

        public void TakeEffect(List<INewEffect> EffectList)
        {
            CurrentEffectList = new LinkedList<List<INewEffect>>();
            CurrentEffectList.AddLast(EffectList);
            ProcessEffect();
        }

        /// <summary>
        /// 时间序列计算
        /// </summary>
        public void ProcessEffect()
        {
            // EffectList的结构为双层列表，第一层代表每个时间步，第二层代表单个时间步内执行同步操作
            TimeStepEffect = CurrentEffectList.First;
            fallList = new List<NewUnit>();

            while (TimeStepEffect != null)
            {
                DealTimeStep(TimeStepEffect);
                TimeStepEffect = TimeStepEffect.Next;
            }
            SetFall();

        }

        /// <summary>
        /// 时间步计算
        /// </summary>
        /// <param name="TimeStepEffect">输入时间步效果列表</param>
        public void DealTimeStep(LinkedListNode<List<INewEffect>> TimeStepEffect)
        {
            HashSet<NewDamage> DamageSet = new HashSet<NewDamage>();

            // 实现当前时间步内效果
            foreach (INewEffect effect in TimeStepEffect.Value)
            {
                //Debug.Log(effect.GetType().Name);

                switch (effect.GetType().Name)
                {
                    case "NewDamage":
                        DealDamage((NewDamage)effect, ref DamageSet);
                        break;
                    default:
                        break;
                }
            }
            // 更新伤害
            UnitTakeDamage(DamageSet);
        }

        /// <summary>
        /// 实现伤害效果
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="DamageDict"></param>
        public void DealDamage(NewDamage effect, ref HashSet<NewDamage> DamageSet)
        {
            NewDamageCalculator.Instance.Calculate(ref effect);

            DamageSet.Add(effect);
        }

        /// <summary>
        /// 在当前时间步后插入临时时间步
        /// 主要用于伤害计算器调用插入剧变反应AOE
        /// </summary>
        /// <param name="newTimeStepEffectList">下一时间步待执行效果</param>
        public void InsertTimeStep(List<INewEffect> newTimeStepEffectList, bool atLast = false)
        {
            if (atLast)
            {
                CurrentEffectList.AddLast(newTimeStepEffectList);
            }
            CurrentEffectList.AddAfter(TimeStepEffect, newTimeStepEffectList);
        }

        /// <summary>
        /// 造成伤害及动画UI更新
        /// </summary>
        /// <param name="DamageDict"></param>
        public void UnitTakeDamage(HashSet<NewDamage> DamageSet)
        {
            // 结算当前时间步所有伤害
            foreach (NewDamage damage in DamageSet)
            {
                //Debug.Log("NewUnit TakeDamage" + damage.damageStructure.DamageValue);
                // 方法内部追加动画阻滞
                bool isFall = damage.ApplyDamage();
                // 判断死亡（流程结束统一实现动画）
                if (isFall)
                {
                    fallList.Add(damage.GetTarget());
                }

            }
        }

        /// <summary>
        /// 设置死亡动画
        /// </summary>
        public void SetFall()
        {
            // 设置死亡
            foreach (NewUnit fallUnit in fallList)
            {
                //fallUnit.SetFall();
            }
        }

    }
}