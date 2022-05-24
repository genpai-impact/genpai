using cfg.effect;  
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 为了大家...
    /// <para>效果：为己方所有场上单位持续一回合的ATKBuff</para>
    /// <para>同元素增强：数值增强</para>
    /// </summary>
    // 2022/4/5 loveYoimiya:
    //      无需选择目标的BuffSpell本应进一步抽象，
    //      但目前已设计好的此种buff只有己方全体加攻击力这一个，难以凭空判断抽象方向
    //      故暂时如此了
    public class Spell_ATKCardBuff_AreaSelfAll_EnhanceNumerical : BaseSpell
    {
        // BaseBuff buff;
        public override void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            base.Init(_elementType, _basaeNumerical, _enhanceNumerical);
            this.Type = TargetType.None;
        }

        public override void Release(Unit sourceUnit, Unit targetUnit)
        {
            /*
            int numerical = BaseNumericalValue;
            if (ElementType == sourceUnit.AtkElement)
            {
                numerical = EnhanceNumericalValue;
            }
            buff = new ATKCardBuff(numerical);

            var effectList = new List<IEffect>();
            var ownUnitList = BattleFieldManager.Instance.CheckOwnUnit(sourceUnit.OwnerSite);
            for (int i = 0; i < ownUnitList.Count; i++)
            {
                if (ownUnitList[i])
                {
                    Unit effectTarget = BattleFieldManager.Instance.Buckets[i].unitCarry;
                    effectList.Add(new AddBuff(sourceUnit, effectTarget, this.buff));
                }
            }
            EffectManager.Instance.TakeEffect(new EffectTimeStep(effectList, TimeEffectType.Spell));
            */
        }
    }
}