namespace Genpai
{
    public class AddBuff : IEffect
    {
        
        public readonly Unit Source;
        public readonly Unit Target;

        public readonly Buff Buff;
        
        public AddBuff(Unit source, Unit target, Buff buff)
        {
            Source = source;
            Target = target;
            Buff = buff;
        }

        public AddBuff(Unit source, Unit target, int buffId, int props = default)
        {
            Source = source;
            Target = target;
            Buff = new Buff(buffId, props);
        }

        public Unit GetSource()
        {
            return Source;
        }

        public Unit GetTarget()
        {
            return Target;
        }

        /// <summary>
        /// 向BuffManager注册Buff-Target
        /// </summary>
        public void Add()
        {
            BuffManager.Instance.AddBuff(Target,Buff);
        }
    }
}