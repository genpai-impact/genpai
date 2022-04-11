
namespace Genpai
{
    public abstract class BaseSpell : ISpell
    {
        public ElementEnum ElementType;

        public SelectTargetType SelectType;

        public int BaseNumericalValue;

        public int EnhanceNumericalValue;


        public virtual void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            ElementType = _elementType;
            BaseNumericalValue = _basaeNumerical;
            EnhanceNumericalValue = _enhanceNumerical;
        }

        public abstract void Release(NewUnit sourceUnit, NewUnit targetUnit);

        public virtual SelectTargetType GetSelectType()
        {
            return SelectType;
        }
    }
}