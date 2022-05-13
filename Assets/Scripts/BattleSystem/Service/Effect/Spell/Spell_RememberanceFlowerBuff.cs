
namespace Genpai
{
    /// <summary>
    /// 追念花团
    /// <para>效果：给一个NotEnemy（己方与Boss）施加护甲buff</para>
    /// <para>同元素增强：更厚的护甲</para>
    /// </summary>
    public class Spell_RememberanceFlowerBuff : SpellBase_BuffNeedSelectTarget_EnhanceNumerical
    {
        public override void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            base.Init(_elementType, _basaeNumerical, _enhanceNumerical);
            SelectType = SelectTargetType.NotEnemy;
        }

        public override void Release(Unit sourceUnit, Unit targetUnit)
        {
            int numerical = BaseNumericalValue;
            if (ElementType == sourceUnit.AtkElement)
            {
                numerical = EnhanceNumericalValue;
            }
            this.buff = new ArmorBuff(numerical);
            base.Release(sourceUnit, targetUnit);
        }
    }
}