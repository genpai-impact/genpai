using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using cfg;
namespace Genpai
{
    /// <summary>
    /// 卡牌读取器，在内存中预存所有卡牌
    /// （数据转换由项目根目录DataScripts/JsonConvert.ipynb实现，修改卡牌类记得匹配修改转换脚本）
    /// </summary>
    public sealed class SkillLoader
    {
        private SkillLoader()
        {
        }
        //读取卡牌结构
        public class SkillData
        {
            public int ID;
            public string SkillName;
            public SkillType SkillType;
            public string SkillDesc;
            public int Cost;
            public string ClassName;
            public string CharName;
            //public UnitType unitType;
        }

        private const string SkillDataPath = "Data/SkillData";
        private const string SkillPath = "Data/HitomiSkill";
        // 更加合理的结构是字典，但是其他地方已经用了list，这里就统一，问题也不大
        public static List<SkillData> SkillDataList = new List<SkillData>();
        //public static List<SkillData> HitomiSkillDataList = new List<SkillData>();
        public static Dictionary<string, List<SkillData>> HitomiSkillDataList = new Dictionary<string, List<SkillData>>();

        public static List<Skill> NewSkills { get; private set; } = new List<Skill>();
        public static void SkillLoad()
        {
            TextAsset text = Resources.Load(SkillDataPath) as TextAsset;
            if (text == null) return;
            string[] textSplit = text.text.Split('\n');
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                SkillData data = new SkillData
                {
                    ID = int.Parse(GetLineTextByIndex(lineSplit, 0)),
                    SkillName = GetLineTextByIndex(lineSplit, 1),
                    SkillType = (SkillType)int.Parse(GetLineTextByIndex(lineSplit, 2)),
                    SkillDesc = GetLineTextByIndex(lineSplit, 3),
                    Cost = int.Parse(GetLineTextByIndex(lineSplit, 4)),
                    ClassName = GetLineTextByIndex(lineSplit, 5)
                };
                SkillDataList.Add(data);
                // Debug.Log(data.SkillDesc);
            }
        }
        public static void MySkillLoad()//怕之前的还有用 就不改原文件了（
        {
            TextAsset text = Resources.Load(SkillPath) as TextAsset;
            /********************CSV被动数据*******************************/
            string[] textSplit = text.text.Split('\n');
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                SkillData data = new SkillData
                {
                    ID = int.Parse(GetLineTextByIndex(lineSplit, 0)),
                    CharName = GetLineTextByIndex(lineSplit, 1),
                    SkillName = GetLineTextByIndex(lineSplit, 2),
                    SkillType = (SkillType)int.Parse(GetLineTextByIndex(lineSplit, 3)),
                    SkillDesc = GetLineTextByIndex(lineSplit, 4),
                    Cost = int.Parse(GetLineTextByIndex(lineSplit, 5))
                };
                //  data.unitType=(UnitType)int.Parse(GetLineTextByIndex(lineSplit, 6));
                if (!HitomiSkillDataList.ContainsKey(data.CharName)) HitomiSkillDataList.Add(data.CharName, new List<SkillData>() { data });
                else HitomiSkillDataList[data.CharName].Add(data);
                //if(data.CharName=="Boss") Debug.Log(data.SkillDesc);
            }
            //Debug.Log(LubanLoader.tables.SkillItems);
            /********************CSV被动数据*******************************/

            /***********************JSON主动及出场*************************/
            foreach (cfg.skill.SkillItem skillItem in LubanLoader.tables.SkillItems.DataList)
            {
                SkillData data = new SkillData
                {
                    ID = skillItem.Id,
                    CharName = skillItem.SkillChara,
                    SkillName = skillItem.SkillName,
                    SkillType = (Genpai.SkillType)skillItem.SkillType,
                    SkillDesc = skillItem.SkillDesc,
                    Cost = skillItem.Cost
                };
                
                if (!HitomiSkillDataList.ContainsKey(data.CharName)) HitomiSkillDataList.Add(data.CharName, new List<SkillData>() { data });
                else HitomiSkillDataList[data.CharName].Add(data);


                // 加载NewSkills
                bool isEruptSkill = skillItem.SkillType == cfg.effect.SkillType.Erupt;
                NewSkills.Add(new Skill(skillItem.Id, skillItem.Cost, isEruptSkill, skillItem.EffectInfos));
            }
            /***********************JSON主动及出场*************************/
            //Debug.Log(HitomiSkillDataList["Boss"].Count);
        }

        private static string GetLineTextByIndex(string[] lineSplit, int index)
        {
            string line = lineSplit[index];
            line = line.Trim();
            line = line.Replace("<br>", "\n");
            return line;
        }

        
        public static void Clean()
        {
            SkillDataList = new List<SkillData>();
            HitomiSkillDataList = new Dictionary<string, List<SkillData>>();
        }
    }
}