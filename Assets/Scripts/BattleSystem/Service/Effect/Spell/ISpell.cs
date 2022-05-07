﻿
namespace Genpai
{
    public interface ISpell
    {
        SelectTargetType GetSelectType();

        void Release(Unit sourceUnit, Unit targetUnit);

        void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical);
    }
}