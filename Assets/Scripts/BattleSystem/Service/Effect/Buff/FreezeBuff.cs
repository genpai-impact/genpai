using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class FreezeBuff : BaseStateEffectBuff, IBuffDeleteable
    {
        public FreezeBuff(int _life = 1)
        {

            BuffName = BuffEnum.Freeze;
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
            Target.ActionState[UnitState.SkillUsing] = force;
            Target.ActionState[UnitState.ChangeChara] = force;
        }

        public override void CheckRemoval(BattleSite site)
        {
            LifeCycles--;
            if (Trigger && Target.OwnerSite == site && LifeCycles <= 0)
            {
                Target.SelfElement = new Element(ElementEnum.Cryo);
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
