﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Buff清单
    /// </summary>
    public enum BuffEnum
    {
        Armor,          //护甲
        Shield,         //护盾
        Burning,        //引燃
        Freeze,         //冻结
        ElectroCharge,  //感电
    }

    /// <summary>
    /// 燃烧Buff
    /// </summary>
    public class Burning : DamageOverTimeBuff, IBuffDeleteable, IBuffIncreasable
    {
        public Burning(int _storey = 1)
        {

            buffName = BuffEnum.Burning;
            buffType = BuffType.DamageOverTimeBuff;

            DamageValue = 1;
            DamageElement = ElementEnum.Pyro;

            storey = _storey;
            Subscribe();
        }

        /// <summary>
        /// 实现烫伤
        /// </summary>
        /// <param name="site"></param>
        public void TakeBurn(BattleSite site)
        {
            if (trigger && target.ownerSite == site)
            {
                LinkedList<List<IEffect>> DamageMessage = new LinkedList<List<IEffect>>();
                List<IEffect> AttackList = new List<IEffect>();
                AttackList.Add(new Damage(null, target, GetDamage()));
                DamageMessage.AddLast(AttackList);

                EffectManager.Instance.TakeEffect(DamageMessage);
            }
        }

        public override void Subscribe()
        {
            // 回合开始时烧单位
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundStart, TakeBurn);
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            trigger = false;
            target.buffAttachment.Remove(this);
        }

        public void IncreaseBuff(int storeys = 0)
        {
            storey += storeys;
        }

        public int GetIncrease()
        {
            return storey;
        }
    }


    /// <summary>
    /// 护甲Buff
    /// 受到伤害不掉层
    /// </summary>
    public class Armor : DamageReduceBuff, IBuffDeleteable, IBuffIncreasable
    {
        public Armor(int _storey)
        {

            buffName = BuffEnum.Armor;
            buffType = BuffType.DamageReduceBuff;

            storey = _storey;
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            target.buffAttachment.Remove(this);
        }

        public int GetIncrease()
        {
            return storey;
        }

        public void IncreaseBuff(int storeys = 0)
        {
            storey += storeys;
        }

        public override int TakeDamage(int damage)
        {
            return System.Math.Max(0, damage - storey);
        }

    }

    /// <summary>
    /// 护盾Buff
    /// 受伤掉层
    /// </summary>
    public class Shield : DamageReduceBuff, IBuffDeleteable, IBuffIncreasable
    {
        public Shield(int _storey)
        {

            buffName = BuffEnum.Armor;
            buffType = BuffType.DamageReduceBuff;

            storey = _storey;
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            target.buffAttachment.Remove(this);
        }

        public int GetIncrease()
        {
            return storey;
        }

        public void IncreaseBuff(int storeys = 0)
        {
            storey += storey;
        }

        public override int TakeDamage(int damage)
        {
            int surplusDamage = System.Math.Max(0, damage - storey);
            storey = System.Math.Max(0, storey - damage);

            return surplusDamage;
        }

    }

    public class ElectroCharge : StateEffectBuff, IBuffDeleteable
    {
        public ElectroCharge(int _life = 1)
        {

            buffName = BuffEnum.ElectroCharge;
            buffType = BuffType.StateEffectBuff;

            LifeCycles = _life;
        }

        public override void AddBuff(UnitEntity _target)
        {
            base.AddBuff(_target);
            EffectState(false);
        }

        public override void EffectState(bool force = false)
        {
            target.ActionState[UnitState.ActiveAttack] = force;
            target.ActionState[UnitState.CounterattackAttack] = force;
        }

        public override void CheckRemoval(BattleSite site)
        {
            if (trigger && target.ownerSite == site && LifeCycles <= 0)
            {
                target.ElementAttachment = new Element(ElementEnum.Electro);
                DeleteBuff();

            }
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            trigger = false;
            target.buffAttachment.Remove(this);
            EffectState(true);
        }
    }

    public class Freeze : StateEffectBuff, IBuffDeleteable
    {
        public Freeze(int _life = 1)
        {

            buffName = BuffEnum.Freeze;
            buffType = BuffType.StateEffectBuff;

            LifeCycles = _life;

        }

        public override void AddBuff(UnitEntity _target)
        {
            base.AddBuff(_target);
            EffectState(false);
        }

        public override void EffectState(bool force = false)
        {
            target.ActionState[UnitState.ActiveAttack] = force;
            target.ActionState[UnitState.CounterattackAttack] = force;
            target.ActionState[UnitState.SkillUsing] = force;
            target.ActionState[UnitState.ChangeChara] = force;
        }

        public override void CheckRemoval(BattleSite site)
        {
            if (target.ownerSite == site && LifeCycles <= 0)
            {
                target.ElementAttachment = new Element(ElementEnum.Cryo);
                DeleteBuff();
            }
        }


        public void DeleteBuff(int deleteStorey = 0)
        {
            trigger = false;
            target.buffAttachment.Remove(this);
            EffectState(true);
        }
    }

}
