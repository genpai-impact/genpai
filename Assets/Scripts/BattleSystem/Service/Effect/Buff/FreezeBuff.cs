
namespace Genpai
{
    public class FreezeBuff : BaseStateEffectBuff, IBuffDeleteable
    {
        public FreezeBuff(int _life = 1)
        {

            buffName = BuffEnum.Freeze;
            buffType = BuffType.StateEffectBuff;

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
            target.ActionState[UnitState.ActiveAttack] = force;
            target.ActionState[UnitState.CounterattackAttack] = force;
            target.ActionState[UnitState.SkillUsing] = force;
            target.ActionState[UnitState.ChangeChara] = force;
        }

        public override void CheckRemoval(BattleSite site)
        {
            LifeCycles--;
            if (target.ownerSite == site && LifeCycles <= 0)
            {
                target.SelfElement = new Element(ElementEnum.Cryo);
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
