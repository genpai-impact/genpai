using System.Collections;
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
    /// 回头把这个转移到其它文件里
    /// </summary>
    public class Burning : DamageOverTimeBuff
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
            if (target.ownerSite == site)
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

    }


    /// <summary>
    /// 护甲Buff
    /// 受到伤害不掉层
    /// </summary>
    public class Armor : DamageReduceBuff
    {
        public Armor(int _storey)
        {

            buffName = BuffEnum.Armor;
            buffType = BuffType.DamageReduceBuff;

            storey = _storey;
        }

        public override int TakeDamage(int damage)
        {
            return System.Math.Max(0, damage - storey);
        }

    }

    /// <summary>
    /// 护甲Buff
    /// 受伤掉层
    /// </summary>
    public class Shield : DamageReduceBuff
    {
        public Shield(int _storey)
        {

            buffName = BuffEnum.Armor;
            buffType = BuffType.DamageReduceBuff;

            storey = _storey;
        }

        public override int TakeDamage(int damage)
        {
            int surplusDamage = System.Math.Max(0, damage - storey);
            storey = System.Math.Max(0, storey - damage);

            return surplusDamage;
        }

    }

    public class ElectroCharge : StateEffectBuff
    {
        public ElectroCharge(int _life = 1)
        {

            buffName = BuffEnum.ElectroCharge;
            buffType = BuffType.StateEffectBuff;

            LifeCycles = _life;
        }

        public override void EffectState()
        {

        }

        public override void Subscribe()
        {
            base.Subscribe();
        }

    }

    public class Freeze : StateEffectBuff
    {
        public Freeze(int _life = 1)
        {

            buffName = BuffEnum.Freeze;
            buffType = BuffType.StateEffectBuff;

            LifeCycles = _life;
        }

        public override void EffectState()
        {

        }

        public override void Subscribe()
        {
            base.Subscribe();
        }

    }

}
