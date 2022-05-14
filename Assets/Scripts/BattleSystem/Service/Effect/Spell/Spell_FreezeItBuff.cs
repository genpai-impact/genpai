using cfg.effect;

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
            Type = TargetType.NotSelf;
        }

        public override void Release(Unit sourceUnit, Unit targetUnit)
        {
            int numerical = BaseNumericalValue;
            if (ElementType == sourceUnit.AtkElement)
            {
                numerical = EnhanceNumericalValue;
            }
            this.buff = new FreezeBuff(numerical);
            base.Release(sourceUnit, targetUnit);
        }
    }
}