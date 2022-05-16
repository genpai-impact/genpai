namespace Genpai
{
    public class NewDelBuff : IEffect
    {
        public readonly Unit Source;
        public readonly Unit Target;

        public readonly int BuffID;
        public readonly int Props;
        
        public NewDelBuff(Unit source, Unit target, int buffID, int num = default)
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

        public void DelBuff()
        {
            BuffManager.Instance.DelBuff(Target,BuffID,Props);
        }
    }
}