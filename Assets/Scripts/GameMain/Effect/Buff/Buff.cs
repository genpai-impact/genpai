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
        public Burning(UnitEntity _target, int _storey)
        {
            target = _target;

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
        public Armor(UnitEntity _target, int _storey)
        {

            target = _target;

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
        public Shield(UnitEntity _target, int _storey)
        {

            target = _target;

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
}
