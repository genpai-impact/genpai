using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// Passive类Effect
    /// 1. DOT
    /// 2. StateEffect
    /// </summary>
    public static partial class BuffEffect
    {
        // --- 减伤 ---
        public static void DamageReduce(BuffPair buffPair, ref int rawDamage)
        {
            int surplusDamage = System.Math.Max(0, rawDamage - buffPair.Buff.Storey);
            
            Debug.Log(buffPair.Buff.BuffAppendix+"Damage Reduced from"+ rawDamage +"to"+ surplusDamage+". Still have Stories "+buffPair.Buff.Storey );
            
            // 护盾会掉层
            if (buffPair.Buff.BuffAppendix == "Shield")
            {
                buffPair.Buff.Storey = System.Math.Max(0, buffPair.Buff.Storey - rawDamage);
                if (buffPair.Buff.Storey == 0) buffPair.IsWorking = false;
            }
            rawDamage = surplusDamage;
        }
        
        // --- 加伤 ---
        public static void AttackBuff(BuffPair buffPair, ref int atk)
        {
            atk += buffPair.Buff.Storey;
        }
        
        // --- 附魔 ---
        public static void AttackElementBuff(BuffPair buffPair, ref ElementEnum element)
        {
            ElementEnum newElement = EnumUtil.ToEnum<ElementEnum>(buffPair.Buff.BuffAppendix);

            // 通过Enum编号大于实现排序
            if (newElement > element)
            {
                element = newElement;
            }
        }
    }
}