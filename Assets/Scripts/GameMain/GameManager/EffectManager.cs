using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 统一效果管理器（伤害、恢复、Buff and so on）
    /// </summary>
    public class EffectManager : Singleton<EffectManager>
    {
        private static readonly object effectHandleLock = new object();

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
        /// 在接收处理请求时自主调用（）
        /// </summary>
        /// <param name="EffectList">待处理效果序列列表</param>
        public void TakeEffect(LinkedList<List<IEffect>> EffectList)
        {
            Debug.Log("Taking Effect");

            CurrentEffectList = EffectList;
            // EffectList的结构为双层列表，第一层代表每个时间步，第二层代表单个时间步内执行同步操作
            TimeStepEffect = EffectList.First;

            // 阵亡单位列表，完成流程后统一依次更新动画
            List<UnitEntity> fallList = new List<UnitEntity>();


            // 进入当前时间步
            while (TimeStepEffect != null)
            {

                Dictionary<UnitEntity, int> DamageDict = new Dictionary<UnitEntity, int>();


                // 实现当前时间步内效果
                // 遍历当前时间步内所有effect，收集更新列表
                foreach (IEffect effect in TimeStepEffect.Value)
                {

                    if (effect is AddBuff)
                    {
                        continue;
                    }

                    if (effect is Damage)
                    {

                        (UnitEntity DamageCarrier, int DamageValue) = DamageCalculator.Instance.Calculate(effect as Damage);
                        Debug.Log(DamageCarrier.unit.unitName + "受到" + DamageValue + "点伤害");
                        DamageDict.Add(DamageCarrier, DamageValue);


                    }
                }

                // 结算时间步伤害
                if (DamageDict.Count != 0)
                {
                    foreach (KeyValuePair<UnitEntity, int> pair in DamageDict)
                    {
                        bool isFall = pair.Key.TakeDamage(pair.Value);
                        // 更新血量并判断死亡（流程结束统一实现动画）
                        if (isFall)
                        {
                            fallList.Add(pair.Key);
                        }

                        // 在单位对应位置播放扣血动画并更新UI
                        pair.Key.GetComponent<UnitDisplay>().DisplayUnit();

                    }
                }

                // 完成更新&动画播放操作

                // 切换至下一时间步
                TimeStepEffect = TimeStepEffect.Next;
            }

            foreach (UnitEntity fallUnit in fallList)
            {
                fallUnit.gameObject.SetActive(false);
            }

        }

        /// <summary>
        /// 在当前时间步后插入临时时间步
        /// 主要用于伤害计算器调用插入剧变反应AOE
        /// </summary>
        /// <param name="newTimeStepEffectList">下一时间步待执行效果</param>
        public void InsertTimeStep(List<IEffect> newTimeStepEffectList)
        {
            CurrentEffectList.AddAfter(TimeStepEffect, newTimeStepEffectList);
        }


    }
}