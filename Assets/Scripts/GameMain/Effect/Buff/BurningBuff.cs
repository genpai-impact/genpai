
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
}
