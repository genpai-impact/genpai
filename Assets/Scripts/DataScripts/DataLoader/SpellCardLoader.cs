using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    public class SpellCardLoader : MonoSingleton<SpellCardLoader>
    {
        private string SpellCardDataPath = "Data\\SpellCardData";

        /// <summary>
        /// key: CardID
        /// <para>value: SpellCardData实例</para>
        /// </summary>
        public Dictionary<int, SpellCardData> SpellCardDataDic = new Dictionary<int, SpellCardData>();

        private void Awake()
        {
        }

        public void LoadSpellCardData()
        {
            TextAsset dataText = Resources.Load(SpellCardDataPath) as TextAsset;
            string[] textSplit = dataText.text.Split('\n');
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                SpellCardData singleData = new SpellCardData();
                singleData.CardID = int.Parse(GetLineTextByIndex(lineSplit, 0));
                singleData.CardName = GetLineTextByIndex(lineSplit, 1);
                singleData.ElementType = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), 
                    GetLineTextByIndex(lineSplit, 2));
                singleData.BaseNumericalValue = int.Parse(GetLineTextByIndex(lineSplit, 3));
                singleData.EnhanceNumericalValue = int.Parse(GetLineTextByIndex(lineSplit, 4));
                singleData.CardInfo = GetLineTextByIndex(lineSplit, 5);
                singleData.ClassName = GetLineTextByIndex(lineSplit, 6);
                SpellCardDataDic.Add(singleData.CardID, singleData);
            }
        }

        private string GetLineTextByIndex(string[] lineSplit, int index)
        {
            string line = lineSplit[index];
            line = line.Trim();
            line = line.Replace("<br>", "\n");
            return line;
        }

        public SpellCard GetSpellCard(int _cardID)
        {
            var spellCardData = SpellCardDataDic[_cardID];
            ISpell spell = ReflectionHelper.CreateInstanceCurrentAssembly<ISpell>(spellCardData.ClassName);
            spell.Init(spellCardData.ElementType, spellCardData.BaseNumericalValue,
                spellCardData.EnhanceNumericalValue);
            string[] paraCardInfo = new string[1] { spellCardData.CardInfo };  // Card类的CardInfo是string[]，故不得不如此
            var newSpellCard = new SpellCard(spellCardData.CardID, "spellCard", spellCardData.CardName, 
                paraCardInfo, spell);
            return newSpellCard;
        }
    }
}
