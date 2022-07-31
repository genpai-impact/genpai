namespace BattleSystem.Service.Effect
{
    public class Cure : IEffect
    {

        public readonly Unit.Unit Source;
        public readonly Unit.Unit Target;
        public readonly int CureValue;

        public Unit.Unit GetSource()
        {
            return Source;
        }

        public Unit.Unit GetTarget()
        {
            return Target;
        }

        public Cure(Unit.Unit source, Unit.Unit target, int cureValue)
        {
            Source = source;
            Target = target;
            CureValue = cureValue;
        }

        public void CureUnit()
        {
            Target.Cured(CureValue);
        }
    }
}
