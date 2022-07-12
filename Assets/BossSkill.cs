using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleSystem.Service.Common;
using BattleSystem.Controller.Unit;
using BattleSystem.Controller.Unit.UnitView;

namespace BattleSystem.Controller.UI
{
    public class BossSkill : MonoBehaviour
    {

        public GameObject[] bossSkill = new GameObject[2];

        public GameObject boss;

        private string bossSkillPath = "ArtAssets/UI/战斗界面/二级菜单/Boss技能/";

        private string bossName;

        // Update is called once per frame
        void Update()
        {
            bossName = boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.UnitName;

            bossSkill[0].transform.Find("布局").gameObject.GetComponent<Image>().sprite = Resources.Load(bossSkillPath+bossName+"1", typeof(Sprite)) as Sprite;
            bossSkill[0].transform.Find("布局").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            bossSkill[0].transform.Find("CD1").Find("内容").gameObject.GetComponent<Text>().text = 
                1-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_1>=0?
                Convert.ToString(1-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_1) : Convert.ToString(0);

            bossSkill[1].transform.Find("布局").gameObject.GetComponent<Image>().sprite = Resources.Load(bossSkillPath+bossName+"2", typeof(Sprite)) as Sprite;
            bossSkill[1].transform.Find("布局").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            bossSkill[1].transform.Find("CD2").Find("内容").gameObject.GetComponent<Text>().text = 
                3-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_2>=0?
                Convert.ToString(3-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_2) : Convert.ToString(0);
        }
    }
}
