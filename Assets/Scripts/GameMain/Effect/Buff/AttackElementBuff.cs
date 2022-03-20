
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
            this.buffName = buffName;
            this.atkElement = atkElement;
            this.buffType = BuffType.StateEffectBuff;
            LifeCycles = lifeCycles;
            Subscribe();
        }

        public override void Effect(BattleSite site)
{
            Debug.Log("Effect " + site + " , " + LifeCycles + " , " + target);
        }

        public override void AddBuff(UnitEntity _target)
        {
            if (_target.ATKElement == ElementEnum.None)
            {
                base.AddBuff(_target);
                Debug.Log("test 2 " + _target + " , " + LifeCycles + " , " + target);
                _target.ATKElement = this.atkElement;
            }
        }

        public override void CheckRemoval(BattleSite site)
        {
            if (1 == 1)
            {
                return;
            }
            Debug.Log("test 1 " + site + " , " + LifeCycles + " , " + base.target);
            if (base.target.ownerSite == site)
            {
                Debug.Log("test " + site + " , " + LifeCycles);
                LifeCycles--;
                if (LifeCycles <= 0)
                {
                    DeleteBuff();
                }
            }
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            target.ATKElement = ElementEnum.None;
            trigger = false;
            target.buffAttachment.Remove(this);
        }
    }
}
