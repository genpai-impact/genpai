﻿
using Messager;
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 燃烧Buff
    /// </summary>
    public class BurningBuff : BaseDamageOverTimeBuff, IBuffDeleteable, IBuffIncreasable
    {
        public BurningBuff(int _storey = 1)
        {

            BuffName = BuffEnum.Burning;
            BuffType = BuffType.DamageOverTimeBuff;

            DamageValue = 1;
            DamageElement = ElementEnum.Pyro;

            Storey = _storey;
            Subscribe();
        }

        /// <summary>
        /// 实现烫伤
        /// </summary>
        /// <param name="site"></param>
        public void TakeBurn(BattleSite site)
        {
            if (Trigger && Target.OwnerSite == site)
            {

                List<IEffect> AttackList = new List<IEffect>();
                AttackList.Add(new Damage(null, Target, GetDamage()));


                EffectManager.Instance.TakeEffect(new EffectTimeStep(AttackList, TimeEffectType.Fixed));
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
            Trigger = false;
            Target.BuffAttachment.Remove(this);
        }

        public void IncreaseBuff(int storeys = 0)
        {
            Storey += storeys;
        }

        public int GetIncrease()
        {
            return Storey;
        }
    }
}
