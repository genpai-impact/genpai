
namespace Genpai
{
    public class ElectroChargeBuff : BaseStateEffectBuff, IBuffDeleteable
    {
        public ElectroChargeBuff(int _life = 1)
        {

            buffName = BuffEnum.ElectroCharge;
            buffType = BuffType.StateEffectBuff;

            LifeCycles = _life;
        }

        public override void AddBuff(NewUnit _target)
        {
            base.AddBuff(_target);
            EffectState(false);
            Subscribe();
        }

        public override void EffectState(bool force = false)
        {
            target.ActionState[UnitState.ActiveAttack] = force;
            target.ActionState[UnitState.CounterattackAttack] = force;
        }

        public override void CheckRemoval(BattleSite site)
        {
            LifeCycles--;
            if (trigger && target.ownerSite == site && LifeCycles <= 0)
            {
                target.SelfElement = new Element(ElementEnum.Electro);
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
