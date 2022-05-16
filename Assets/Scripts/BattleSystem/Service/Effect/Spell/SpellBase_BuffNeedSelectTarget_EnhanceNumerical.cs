﻿
using System.Collections.Generic;

namespace Genpai
{
    public abstract class SpellBase_BuffNeedSelectTarget_EnhanceNumerical : BaseSpell
    {
        /// <summary>
        /// 要施加的buff
        /// </summary>
        // protected BaseBuff buff;

        public override void Release(Unit sourceUnit, Unit targetUnit)
        {
            var effectList = new List<IEffect>();
            // effectList.Add(new AddBuff(sourceUnit, targetUnit, buff));
            EffectManager.Instance.TakeEffect(new EffectTimeStep(effectList, TimeEffectType.Spell));
        }
    }
}