
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 魔法：攻击一个目标
    /// <para>同元素增强：攻击变为元素攻击</para>
    /// </summary>
    public class Spell_AttackSingleTarget_EnhanceElement : BaseSpell
    {
        public override void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            base.Init(_elementType, _basaeNumerical, _enhanceNumerical);
            SelectType = SelectTargetType.NotSelf;
        }

        public override void Release(Unit sourceUnit, Unit targetUnit)
        {
            var attackElementType = ElementEnum.None;
            if (ElementType == sourceUnit.ATKElement)
            {
                attackElementType = ElementType;
            }
            var effectList = new List<IEffect>();
            effectList.Add(new Damage(sourceUnit, targetUnit,
                new DamageStruct(BaseNumericalValue, attackElementType)));
            EffectManager.Instance.TakeEffect(new EffectTimeStep(effectList, TimeEffectType.Spell));
        }
    }
}