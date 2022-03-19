using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public enum TargetType
    {
        None,
        Enemy,
        Self,
        All,
        NotEnemy
    }

    public enum TargetArea
    {
        Mono,
        All,
        AOE,
        SelfAll,
        None
    }
    //读取卡牌结构
    public class SpellCardData
    {
        public int ID;
        public string magicName;
        public ElementEnum elementType;
        public SpellType magicType;
        public object MagicTypeAppendix;
        public TargetType targetType;
        public TargetArea targetArea;
        public int BaseNumerical;
        public SpellElementBuff elementBuff;
        public object ElementBuffAppendix;
        public string CardInfo;
    }

    public class SpellCardLoader : MonoSingleton<SpellCardLoader>
    {
        private string SpellCardDataPath = "Data\\SpellCardData";

        public List<SpellCardData> SpellCardDataList = new List<SpellCardData>();

        private void Awake()
        {
        }
        public void SpellCardLoad()
        {
            TextAsset text = Resources.Load(SpellCardDataPath) as TextAsset;
            string[] textSplit = text.text.Split('\n');
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                SpellCardData data = new SpellCardData();
                data.ID = int.Parse(GetLineTextByIndex(lineSplit, 0));
                data.magicName = GetLineTextByIndex(lineSplit, 1);
                data.elementType = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), GetLineTextByIndex(lineSplit, 2));
                data.magicType = (SpellType)System.Enum.Parse(typeof(SpellType), GetLineTextByIndex(lineSplit, 3));
                data.MagicTypeAppendix = GetLineTextByIndex(lineSplit, 4);
                data.targetType = (TargetType)System.Enum.Parse(typeof(TargetType), GetLineTextByIndex(lineSplit, 5));
                data.targetArea = (TargetArea)System.Enum.Parse(typeof(TargetArea), GetLineTextByIndex(lineSplit, 6));
                data.BaseNumerical = int.Parse(GetLineTextByIndex(lineSplit, 7));
                data.elementBuff = (SpellElementBuff)System.Enum.Parse(typeof(SpellElementBuff), GetLineTextByIndex(lineSplit, 8));
                data.ElementBuffAppendix = GetLineTextByIndex(lineSplit, 9);
                data.CardInfo = GetLineTextByIndex(lineSplit, 10);
                SpellCardDataList.Add(data);
            }
        }

        private string GetLineTextByIndex(string[] lineSplit, int index)
        {
            string line = lineSplit[index];
            line = line.Trim();
            line = line.Replace("<br>", "\n");
            return line;
        }

        public SpellCard GetSpellCard(int _index,int cardID)
        {
            //Debug.Log(cardID);
            SpellCardData data = SpellCardDataList[_index - 1];
            switch (data.magicType)
            {
                case SpellType.Damage:
                    return new DamageSpellCard(cardID, "spellCard",data);
                case SpellType.Cure:
                    return new CureSpellCard(cardID, "spellCard", data);
                case SpellType.Buff:
                    return new BuffSpellCard(cardID, "spellCard", data);
                //return new BuffSpellCard(cardID, "spellCard", data.magicName, data.CardInfo.Split('\n'),
                //    SpellType.Buff, data.elementType, data.BaseNumerical,buffName);
                case SpellType.Draw:
                    return new DrawSpellCard(cardID, "spellCard", data);
            }
            return null;
        }
    }
}
