namespace Genpai
{
    public class DelBuff : IEffect
    {
        public readonly Unit Source;
        public readonly Unit Target;

        public readonly int BuffID;
        public readonly int Props;
        
        public DelBuff(Unit source, Unit target, int buffID, int num = default)
        {
            Source = source;
            Target = target;
            BuffID = buffID;
            Props = num;
        }

        public Unit GetSource()
        {
            return Source;
        }

        public Unit GetTarget()
        {
            return Target;
        }

        public void Del()
        {
            BuffManager.Instance.DelBuff(Target,BuffID,Props);
        }
    }
}