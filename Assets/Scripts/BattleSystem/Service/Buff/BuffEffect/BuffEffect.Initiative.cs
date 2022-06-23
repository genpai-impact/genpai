using System.Collections.Generic;
using BattleSystem.Service.Effect;
using BattleSystem.Service.Element;
using BattleSystem.Service.Unit;
using Utils;

namespace BattleSystem.Service.Buff.BuffEffect
{
    /// <summary>
    /// Initiative类Effect
    /// 1. DOT
    /// 2. StateEffect
    /// </summary>
    public static partial class BuffEffect
    {
        // --- DOT ---
        public static void DamageOverTime(BuffPair buffPair, ref List<IEffect> dot)
        {
            if (!buffPair.IsWorking) return;
            var elementEnum = EnumUtil.ToEnum<ElementEnum>(buffPair.Buff.BuffAppendix);
            
            dot.Add(new Damage(null,buffPair.Unit,new DamageStruct(buffPair.Buff.Storey,elementEnum)));
        }
        
        // --- StateEffect ---

        private static readonly HashSet<UnitState> FreezeState = new HashSet<UnitState>()
        {
            UnitState.ActiveAttack,
            UnitState.CounterattackAttack,
            UnitState.SkillUsing,
            UnitState.ChangeChara,
        };
        
        private static readonly HashSet<UnitState> ElectroChargeState = new HashSet<UnitState>()
        {
            UnitState.ActiveAttack,
            UnitState.CounterattackAttack,
        };

        public static void StateEffect(BuffPair buffPair)
        {
            HashSet<UnitState> states = new HashSet<UnitState>();
            switch (buffPair.Buff.BuffAppendix)
            {
                case "Freeze":
                    states = FreezeState;
                    break;
                case "ElectroCharge":
                    states = ElectroChargeState;
                    break;
            }

            foreach (var unitState in states)
            {
                buffPair.Unit.ActionState[unitState] = false;
            }
        }
    }
}