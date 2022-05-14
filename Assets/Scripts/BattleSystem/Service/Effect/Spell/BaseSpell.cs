using cfg.effect;  

namespace Genpai
{
    public abstract class BaseSpell : ISpell
    {
        public ElementEnum ElementType;

        public TargetType Type;

        public int BaseNumericalValue;

        public int EnhanceNumericalValue;


        public virtual void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            ElementType = _elementType;
            BaseNumericalValue = _basaeNumerical;
            EnhanceNumericalValue = _enhanceNumerical;
        }

        public abstract void Release(Unit sourceUnit, Unit targetUnit);

        public virtual TargetType GetSelectType()
        {
            return Type;
        }
    }
}