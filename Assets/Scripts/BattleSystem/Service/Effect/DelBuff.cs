using BattleSystem.Service.Buff;

namespace BattleSystem.Service.Effect
{
    public class DelBuff : IEffect
    {
        public readonly Unit.Unit Source;
        public readonly Unit.Unit Target;

        public readonly int BuffID;
        public readonly int Props;
        
        public DelBuff(Unit.Unit source, Unit.Unit target, int buffID, int num = default)
        {
            Source = source;
            Target = target;
            BuffID = buffID;
            Props = num;
        }

        public Unit.Unit GetSource()
        {
            return Source;
        }

        public Unit.Unit GetTarget()
        {
            return Target;
        }

        public void Del()
        {
            BuffManager.Instance.DelBuff(Target,BuffID,Props);
        }
    }
}