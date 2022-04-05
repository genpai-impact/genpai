
namespace Genpai
{
    public interface ISpell
    {
        SelectTargetType GetSelectType();

        void Release(UnitEntity sourceUnit, UnitEntity targetUnit);

        void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical);
    }
}