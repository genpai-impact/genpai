using UnityEngine;

namespace Genpai
{
    public class ElectroChargeBuff : BaseStateEffectBuff, IBuffDeleteable
    {
        public ElectroChargeBuff(int _life = 1)
        {
            BuffName = BuffEnum.ElectroCharge;
            BuffType = BuffType.StateEffectBuff;

            LifeCycles = _life;
        }

        public override void AddBuff(Unit _target)
        {
            base.AddBuff(_target);
            EffectState(false);
            Subscribe();
        }

        public override void EffectState(bool force = false)
        {
            Target.ActionState[UnitState.ActiveAttack] = force;
            Target.ActionState[UnitState.CounterattackAttack] = force;
        }

        public override void CheckRemoval(BattleSite site)
        {
            LifeCycles--;
            if (Trigger && Target.OwnerSite == site && LifeCycles <= 0)
            {
                Target.SelfElement = new Element(ElementEnum.Electro);
                DeleteBuff();
            }
        }

        public void DeleteBuff(int deleteStorey = 0)
        {
            Trigger = false;
            Target.BuffAttachment.Remove(this);
            EffectState(true);
        }
    }
}