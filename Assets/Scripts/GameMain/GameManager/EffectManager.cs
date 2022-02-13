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

        public List<UnitEntity> fallList;

        /// <summary>
        /// 效果序列处理函数
        /// </summary>
        /// <param name="EffectList">待处理效果序列列表</param>
        public void TakeEffect(LinkedList<List<IEffect>> EffectList)
        {
            lock (effectHandleLock)
            {
                CurrentEffectList = EffectList;

                ProcessEffect();
            }

        }

        public void TakeEffect(List<IEffect> EffectList)
        {
            lock (effectHandleLock)
            {
                CurrentEffectList = new LinkedList<List<IEffect>>();
                CurrentEffectList.AddLast(EffectList);

                ProcessEffect();
            }

        }

        /// <summary>
        /// 时间序列计算
        /// </summary>
        public void ProcessEffect()
        {

            // EffectList的结构为双层列表，第一层代表每个时间步，第二层代表单个时间步内执行同步操作
            TimeStepEffect = CurrentEffectList.First;

            fallList = new List<UnitEntity>();

            // 进入当前时间步
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
        public void DealTimeStep(LinkedListNode<List<IEffect>> TimeStepEffect)
        {
            Dictionary<UnitEntity, int> DamageDict = new Dictionary<UnitEntity, int>();

            // 实现当前时间步内效果
            foreach (IEffect effect in TimeStepEffect.Value)
            {
                switch (effect.GetType().Name)
                {
                    case "AddBuff":
                        ((AddBuff)effect).Add();
                        break;
                    case "DelBuff":
                        ((DelBuff)effect).Remove();
                        break;
                    case "Damage":
                        DealDamage((Damage)effect, ref DamageDict);
                        break;
                    default:
                        break;
                }
            }

            // 更新伤害
            UnitTakeDamage(DamageDict);
        }

        /// <summary>
        /// 实现伤害效果
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="DamageDict"></param>
        public void DealDamage(Damage effect, ref Dictionary<UnitEntity, int> DamageDict)
        {
            // 播放攻击动画
            effect.source.GetComponent<UnitDisplay>().AttackAnimation();

            // 计算伤害
            (UnitEntity DamageCarrier, int DamageValue) = DamageCalculator.Instance.Calculate(effect);

            DamageDict.Add(DamageCarrier, DamageValue);
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

        /// <summary>
        /// 造成伤害及UI更新
        /// </summary>
        /// <param name="DamageDict"></param>
        public void UnitTakeDamage(Dictionary<UnitEntity, int> DamageDict)
        {
            if (DamageDict.Count == 0)
            {
                return;
            }
            // 结算时间步伤害
            foreach (KeyValuePair<UnitEntity, int> pair in DamageDict)
            {
                bool isFall = pair.Key.TakeDamage(pair.Value);
                Debug.Log(pair.Key.unit.unitName + "受到" + pair.Value + "点伤害");

                // 更新血量并判断死亡（流程结束统一实现动画）
                if (isFall) { fallList.Add(pair.Key); }

                // 在单位对应位置播放扣血动画并更新UI
                pair.Key.GetComponent<UnitDisplay>().FreshUnitUI();

            }

        }

        /// <summary>
        /// 设置死亡动画
        /// </summary>
        public void SetFall()
        {
            // 设置死亡
            foreach (UnitEntity fallUnit in fallList)
            {
                fallUnit.unit = null;
                fallUnit.gameObject.SetActive(false);
            }
        }

    }
}