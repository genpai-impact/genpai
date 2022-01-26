using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 统一效果管理器（伤害、恢复、Buff and so on）
    /// （单位受伤及下场是否由此统一管理？Boss等事件消息是否需要额外设立UnitManager进行管理？当前方案为由单位自行执行，不够合理）
    /// </summary>
    public class EffectManager : Singleton<EffectManager>
    {
        /// <summary>
        /// 当前正在处理效果序列
        /// </summary>
        public LinkedList<List<IEffect>> CurrentEffectList;

        /// <summary>
        /// 当前正在执行时间步
        /// </summary>
        public LinkedListNode<List<IEffect>> TimeStepEffect;

        /// <summary>
        /// 效果序列处理函数
        /// </summary>
        /// <param name="EffectList">待处理效果序列列表</param>
        public void TakeEffect(LinkedList<List<IEffect>> EffectList)
        {

            CurrentEffectList = EffectList;
            // EffectList的结构为双层列表，第一层代表每个时间步，第二层代表单个时间步内执行同步操作
            TimeStepEffect = EffectList.First;

            while (TimeStepEffect != null)
            {

                // 遍历当前时间步内所有effect
                foreach (IEffect effect in TimeStepEffect.Value)
                {

                    if (effect is AddBuff)
                    {

                    }
                    if (effect is Damage)
                    {
                        // 调用伤害计算器
                        int DamageValue = (effect as Damage).damage.DamageValue;
                        UnitEntity DamageCarrier;
                        (DamageValue, DamageCarrier) = DamageCalculator.Instance.Calculate(effect as Damage);

                    }
                }

                TimeStepEffect = TimeStepEffect.Next;
            }

        }

        /// <summary>
        /// 在当前时间步后插入临时时间步
        /// 主要用于插入剧变反应AOE
        /// </summary>
        /// <param name="newTimeStepEffectList">下一时间步</param>
        public void InsertTimeStep(List<IEffect> newTimeStepEffectList)
        {
            CurrentEffectList.AddAfter(TimeStepEffect, newTimeStepEffectList);
        }



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}