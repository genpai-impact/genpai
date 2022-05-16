namespace Genpai
{
    public class NewAddBuff : IEffect
    {
        
        public readonly Unit Source;
        public readonly Unit Target;

        public readonly Buff Buff;
        
        public NewAddBuff(Unit source, Unit target, Buff buff)
        {
            Source = source;
            Target = target;
            Buff = buff;
        }

        public NewAddBuff(Unit source, Unit target, int buffId, int props)
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
        /// TODO: 速速实现BuffManager
        /// </summary>
        public void AddBuff()
        {
            BuffManager.Instance.AddBuff(Target,Buff);
        }
    }
}