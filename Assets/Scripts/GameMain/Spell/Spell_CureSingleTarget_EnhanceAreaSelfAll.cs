
using System.Collections.Generic;

namespace Genpai
{
    /// <summary>
    /// 魔法：治疗一个己方目标
    /// <para>同元素增强：治疗己方全体单位</para>
    /// </summary>
    public class Spell_CureSingleTarget_EnhanceAreaSelfAll : BaseSpell
    {
        public override void Init(ElementEnum _elementType, int _basaeNumerical, int _enhanceNumerical)
        {
            base.Init(_elementType, _basaeNumerical, _enhanceNumerical);
            SelectType = SelectTargetType.Self;
        }

        public override void Release(NewUnit sourceUnit, NewUnit targetUnit)
        {
            //var effectList = new LinkedList<List<IEffect>>();
            var effectList = new List<IEffect>();
            if (ElementType != sourceUnit.ATKElement)
            {
                effectList.Add(new Cure(sourceUnit, targetUnit, BaseNumericalValue));
            }
            else
            {
                var ownUnitList = BattleFieldManager.Instance.CheckOwnUnit(sourceUnit.ownerSite);
                for (int i = 0; i < ownUnitList.Count; i++)
                {
                    if (ownUnitList[i])
                    {
                        NewUnit cureTarget = NewBattleFieldManager.Instance.buckets[i].unitCarry;
                        effectList.Add(new Cure(sourceUnit, cureTarget, BaseNumericalValue));
                    }
                }
            }
            EffectManager.Instance.TakeEffect(effectList);
        }
    }
}