
using Messager;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Genpai
{
    /// <summary>
    /// 攻击类型buff
    /// </summary>
    public class AttackElementBuff : BaseStateEffectBuff, IBuffDeleteable
    {
        private ElementEnum atkElement;
        public AttackElementBuff(BuffEnum buffName, ElementEnum atkElement, int lifeCycles)
        {
            this.BuffName = buffName;
            this.atkElement = atkElement;
            this.BuffType = BuffType.StateEffectBuff;
            LifeCycles = lifeCycles;
            Subscribe();
        }

        public override void Effect(BattleSite site)
        {
            Debug.Log("Effect " + site + " , " + LifeCycles + " , " + Target);
        }

        public override void AddBuff(Unit _target)
        {
            if (_target.AtkElement == ElementEnum.None)
            {
                base.AddBuff(_target);
            }
        }

        public override void CheckRemoval(BattleSite site)
        {
            // fixme 似乎一个回合会走两次这里，有bug
            if (Target == null)
            {
                return;
            }
            if (base.Target.OwnerSite == site)
            {
                LifeCycles--;
                if (LifeCycles <= 0)
                {
                    DeleteBuff();
                }
            }
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            Trigger = false;
            Target.BuffAttachment.Remove(this);
        }
    }
}
