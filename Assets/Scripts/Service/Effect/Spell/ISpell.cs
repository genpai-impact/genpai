
namespace Genpai
{
    public interface ISpell
    {
        SelectTargetType GetSelectType();

        void Release(NewUnit sourceUnit, NewUnit targetUnit);

        void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical);
    }
}