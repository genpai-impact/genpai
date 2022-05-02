using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

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

        private const string SkillDataPath = "Data\\SkillData";
        private const string skillPath = "Data/HitomiSkill";
        // 更加合理的结构是字典，但是其他地方已经用了list，这里就统一，问题也不大
        public static List<SkillData> SkillDataList = new List<SkillData>();
        //public static List<SkillData> HitomiSkillDataList = new List<SkillData>();
        public static Dictionary<string,List<SkillData>> HitomiSkillDataList = new Dictionary<string, List<SkillData>>();
        public static void SkillLoad()
        {
            TextAsset text = Resources.Load(SkillDataPath) as TextAsset;
            string[] textSplit = text.text.Split('\n');
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                SkillData data = new SkillData();
                data.ID = int.Parse(GetLineTextByIndex(lineSplit, 0));
                data.SkillName = GetLineTextByIndex(lineSplit, 1);
                data.SkillType = (SkillType)int.Parse(GetLineTextByIndex(lineSplit, 2));
                data.SkillDesc = GetLineTextByIndex(lineSplit, 3);
                data.Cost = int.Parse(GetLineTextByIndex(lineSplit, 4));
                data.ClassName = GetLineTextByIndex(lineSplit, 5);
                SkillDataList.Add(data);
               // Debug.Log(data.SkillDesc);
            }
        }
        public static void MySkillLoad()//怕之前的还有用 就不改原文件了（
        {
            TextAsset text = Resources.Load(skillPath) as TextAsset;
            string[] textSplit = text.text.Split('\n');
            foreach (var line in textSplit)
            {
                string[] lineSplit = line.Split(',');
                SkillData data = new SkillData();
                data.ID = int.Parse(GetLineTextByIndex(lineSplit, 0));
                data.CharName = GetLineTextByIndex(lineSplit, 1);
                data.SkillName = GetLineTextByIndex(lineSplit, 2);
                data.SkillType = (SkillType)int.Parse(GetLineTextByIndex(lineSplit, 3));
                data.SkillDesc = GetLineTextByIndex(lineSplit, 4);
                data.Cost = int.Parse(GetLineTextByIndex(lineSplit, 5));
              //  data.unitType=(UnitType)int.Parse(GetLineTextByIndex(lineSplit, 6));
                if (!HitomiSkillDataList.ContainsKey(data.CharName)) HitomiSkillDataList.Add(data.CharName, new List<SkillData>() { data });
                else HitomiSkillDataList[data.CharName].Add(data);
              //if(data.CharName=="Boss") Debug.Log(data.SkillDesc);
            }
          //  Debug.Log(HitomiSkillDataList["Boss"].Count);
        }

        private static string GetLineTextByIndex(string[] lineSplit, int index)
        {
            string line = lineSplit[index];
            line = line.Trim();
            line = line.Replace("<br>", "\n");
            return line;
        }

        public static ISkill GetSkill(int skillId)
        {
            for (int i = 0; i < SkillDataList.Count; i++)
            {
                SkillData data = SkillDataList[i];
                if (data.ID == skillId)
                {
                    ISkill skill = ReflectionHelper.CreateInstanceCurrentAssembly<ISkill>(data.ClassName);
                    skill.Init(data.ID, data.SkillName, data.SkillType, data.SkillDesc, data.Cost);
                    return skill;
                }
            }
            throw new System.Exception("找不到对应技能");
        }
        public static void clean()
        {
            SkillDataList = new List<SkillData>();
            HitomiSkillDataList = new Dictionary<string, List<SkillData>>();
        }
    }
}