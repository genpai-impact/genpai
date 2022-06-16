using System;
using UnityEngine;

namespace Genpai
{
    public sealed class EnumUtil
    {
        public static CardType CardTypeToUnitType(cfg.card.CardType cardType)
        {
            switch (cardType)
            {
                case cfg.card.CardType.Monster: return CardType.Monster;
                case cfg.card.CardType.Boss: return CardType.Boss;
                case cfg.card.CardType.Chara: return CardType.Chara;
                default: throw new System.Exception("无法转换");
            }
        }
        public static T ToEnum<T>(string str)
        {
            T ret = default;
            try
            {
                ret = (T)Enum.Parse(typeof(T), str);  // 之前少了这一行导致攻击卡无法带元素
            }
            catch
            {
                Debug.Log(str+"无法转换至"+typeof(T));
            }

            return ret;
        }
    }
}
