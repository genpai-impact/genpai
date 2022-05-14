
using System;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
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
                ret = (T)Enum.Parse(typeof(T), str);
            }
            catch
            {
                Debug.Log(str+"无法转换至"+typeof(T));
            }

            return ret;
        }
    }
}
