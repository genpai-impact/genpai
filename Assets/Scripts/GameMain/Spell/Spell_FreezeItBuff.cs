
namespace Genpai
{
    /// <summary>
    /// 冻结吧！
    /// <para>魔法：给一个NotSelf单位施加持续一定回合的冻结Buff</para>
    /// <para>同元素增强：给一个NotSelf单位施加持续更多回合的冻结Buff</para>
    /// </summary>
    public class Spell_FreezeItBuff : SpellBase_BuffNeedSelectTarget_EnhanceNumerical
    {
        public override void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            base.Init(_elementType, _basaeNumerical, _enhanceNumerical);
            SelectType = SelectTargetType.NotSelf;
        }

        public override void Release(UnitEntity sourceUnit, UnitEntity targetUnit)
        {
            int numerical = BaseNumericalValue;
            if (ElementType == sourceUnit.ATKElement)
            {
                numerical = EnhanceNumericalValue;
            }
            this.buff = new FreezeBuff(numerical);
            base.Release(sourceUnit, targetUnit);
        }
    }
}