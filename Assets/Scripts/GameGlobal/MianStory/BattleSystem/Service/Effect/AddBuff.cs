using BattleSystem.Service.Buff;

namespace BattleSystem.Service.Effect
{
    public class AddBuff : IEffect
    {
        
        public readonly Unit.Unit Source;
        public readonly Unit.Unit Target;

        public readonly Buff.Buff Buff;
        
        public AddBuff(Unit.Unit source, Unit.Unit target, Buff.Buff buff)
        {
            Source = source;
            Target = target;
            Buff = buff;
        }

        public AddBuff(Unit.Unit source, Unit.Unit target, int buffId, int props = default)
        {
            Source = source;
            Target = target;
            Buff = new Buff.Buff(buffId, props);
        }

        public Unit.Unit GetSource()
        {
            return Source;
        }

        public Unit.Unit GetTarget()
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