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
        public LinkedList<EffectTimeStep> CurrentEffectList;

        /// <summary>
        /// 当前正在执行时间步
        /// </summary>
        public LinkedListNode<EffectTimeStep> TimeStepEffect;

        /// <summary>
        /// 待更新死亡清单
        /// </summary>
        public List<Unit> fallList;

        /// <summary>
        /// 效果序列处理函数
        /// </summary>
        /// <param name="EffectList">待处理效果序列列表</param>
        public void TakeEffect(LinkedList<EffectTimeStep> EffectList)
        {
            CurrentEffectList = EffectList;
            ProcessEffect();
        }

        public void TakeEffect(EffectTimeStep EffectList)
        {
            CurrentEffectList = new LinkedList<EffectTimeStep>();
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
            fallList = new List<Unit>();

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
        public void DealTimeStep(LinkedListNode<EffectTimeStep> TimeStepEffect)
        {
            HashSet<Damage> DamageSet = new HashSet<Damage>();

            // 实现当前时间步内效果
            foreach (IEffect effect in TimeStepEffect.Value.EffectList)
            {
                // Debug.Log(effect.GetType().Name);

                switch (effect.GetType().Name)
                {
                    case "AddBuff":
                        ((AddBuff)effect).Add();
                        AnimatorManager.Instance.InsertAnimator(effect, "addbuff");
                        break;
                    case "DelBuff":
                        ((DelBuff)effect).Remove();
                        AnimatorManager.Instance.InsertAnimator(effect, "delbuff");
                        break;
                    case "Damage":
                        DealDamage((Damage)effect, ref DamageSet);
                        break;
                    case "ReactionDamage":
                        DealDamage((Damage)effect, ref DamageSet);
                        break;
                    case "Cure":
                        ((Cure)effect).CureUnit();
                        break;
                    default:
                        break;
                }
                //BucketEntityManager.Instance.GetUnitEntityByUnit(effect.GetTarget()).UnitDisplay.FreshUnitUI(effect.GetTarget().GetView());
            }
            // 更新伤害
            UnitTakeDamage(DamageSet);

        }

        /// <summary>
        /// 实现伤害效果
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="DamageDict"></param>
        public void DealDamage(Damage effect, ref HashSet<Damage> DamageSet)
        {
            DamageCalculator.Instance.Calculate(ref effect);
            DamageSet.Add(effect);
        }

        /// <summary>
        /// 在当前时间步后插入临时时间步
        /// 主要用于伤害计算器调用插入剧变反应AOE
        /// </summary>
        /// <param name="newTimeStepEffectList">下一时间步待执行效果</param>
        public void InsertTimeStep(EffectTimeStep newTimeStepEffectList, bool atLast = false)
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
        public void UnitTakeDamage(HashSet<Damage> DamageSet)
        {
            // 结算当前时间步所有伤害
            foreach (Damage damage in DamageSet)
            {
                if (damage.target.isFall)
                {
                    continue;
                }

                bool isFall = damage.ApplyDamage();
                // if(isFall) Debug.Log(damage.GetTarget());

                // TODO：动画管理器
                if (damage.damageStructure.DamageValue > 0)
                {
                    if (damage.damageType == DamageType.NormalAttack)
                        BucketEntityManager.Instance.GetUnitEntityByUnit(damage.GetSource()).UnitModelDisplay.AttackAnimation(damage);
                    if (damage.damageType == DamageType.Reaction)
                        BucketEntityManager.Instance.GetUnitEntityByUnit(damage.GetSource()).UnitModelDisplay.ReactionAnimation(damage);
                    if (!isFall)
                    {
                        BucketEntityManager.Instance.GetUnitEntityByUnit(damage.GetTarget()).UnitModelDisplay.InjuredAnimation(damage);
                    }
                }

                // BucketEntityManager.Instance.GetUnitEntityByUnit(damage.GetTarget()).UnitDisplay.FreshUnitUI(damage.GetTarget().GetView());
                // if (damage.damageStructure.DamageValue <= 0)
                // BucketEntityManager.Instance.GetUnitEntityByUnit(damage.GetTarget()).UnitModelDisplay.ReactionAnimation(damage);

                // 判断死亡（流程结束统一实现动画）
                if (isFall)
                {
                    fallList.Add(damage.GetTarget());
                }
                else
                {
                    // UI更新
                    if (damage.target.unitType == UnitType.Chara)
                    {
                        // GameContext.Instance.GetPlayerBySite(damage.target.ownerSite).CharaManager.RefreshCharaUI(damage.target.GetView());
                    }
                }
            }
        }

        /// <summary>
        /// 设置死亡动画
        /// </summary>
        public void SetFall()
        {
            // TODO：死亡动画
            // 设置死亡
            foreach (Unit fallUnit in fallList)
            {
                // BucketEntityManager.Instance.GetUnitEntityByUnit(fallUnit).GetComponent<UnitDisplay>().Init(null);
                // Damage fallDamage = new Damage(fallUnit, fallUnit, new DamageStruct(0, ElementEnum.None));
                AnimatorManager.Instance.InsertAnimator(BucketEntityManager.Instance.GetUnitEntityByUnit(fallUnit).GetComponent<UnitDisplay>(), "fall");
                fallUnit.SetFall();
            }
        }

    }
}