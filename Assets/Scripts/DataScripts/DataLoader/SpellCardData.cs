﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
using Excel;

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
        None
    }
    //读取卡牌结构
    public class SpellCardData
    {
        public int ID;
        public string magicName;
        public ElementEnum elementType;
        public SpellType magicType;
        public TargetType targetType;
        public TargetArea targetArea;
        public int BaseNumerical;
        public SpellElementBuff elementBuff;
        public string CardInfo;
    }

    public class SpellCardLoader : MonoSingleton<SpellCardLoader>
    {
        private string SpellCardDataPath = "Assets\\Resources\\Data\\SpellCardData.xlsx";

        public List<SpellCardData> SpellCardDataList = new List<SpellCardData>();

        private void Awake()
        {
            SpellCardDataList = SpellCardLoad();
        }
        public List<SpellCardData> SpellCardLoad()
        {
            FileStream fs = File.Open(SpellCardDataPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader iExcelDR = ExcelReaderFactory.CreateOpenXmlReader(fs);
            DataSet ds = iExcelDR.AsDataSet();
            DataTable table = ds.Tables[0];

            List<SpellCardData> dataList = new List<SpellCardData>();
            int rows = table.Rows.Count;
            Debug.Log(rows);
            for (int i = 1; i < 14; i++)
            {
                SpellCardData data = new SpellCardData();

                data.ID = int.Parse(table.Rows[i][0].ToString());
                data.magicName = table.Rows[i][1].ToString();
                data.elementType = (ElementEnum)System.Enum.Parse(typeof(ElementEnum), table.Rows[i][2].ToString());
                data.magicType = (SpellType)System.Enum.Parse(typeof(SpellType), table.Rows[i][3].ToString());

                data.targetType = (TargetType)System.Enum.Parse(typeof(TargetType), table.Rows[i][5].ToString());
                data.targetArea = (TargetArea)System.Enum.Parse(typeof(TargetArea), table.Rows[i][6].ToString());
                data.BaseNumerical = int.Parse(table.Rows[i][7].ToString());
                data.elementBuff = (SpellElementBuff)System.Enum.Parse(typeof(SpellElementBuff), table.Rows[i][8].ToString());

                data.CardInfo = table.Rows[i][10].ToString();

                dataList.Add(data);
            }
            return dataList;
        }
    }
}
