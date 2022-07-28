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
    /// <summary>
    /// 为已有的Boss技能部分添加图片和cd，挂在 BattleScene的UI/BOSS相关/技能 下
    /// 关于CD：直接用实际MP和所需MP的差值作为CD，因为暂时没找到CD的要求
    /// 凹凸曼：2022/7/12
    /// </summary>
    public class BossSkill : MonoBehaviour
    {
        // 技能1，技能2。其实可以直接用更细致的数组，这样就不用在后续代码中疯狂”Find“:) 
        public GameObject[] bossSkill = new GameObject[2];

        // boss的游戏对象
        public GameObject boss;

        // BOSS技能图片资源路径
        private string bossSkillPath = "ArtAssets/UI/战斗界面/二级菜单/Boss技能/";

        // BOSS名称
        private string bossName;

        // 更新BOSS技能、CD
        void Update()
        {
            // 获取当前BOSS名称
            bossName = boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.UnitName;

            // 获取技能1的图片
            bossSkill[0].transform.Find("布局").gameObject.GetComponent<Image>().sprite = Resources.Load(bossSkillPath+bossName+"1", typeof(Sprite)) as Sprite;
            bossSkill[0].transform.Find("布局").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

            // 更新技能1的CD，直接用Mp_1和需要Mp的差值，这两个参数设定在Boss类下
            bossSkill[0].transform.Find("CD1").Find("内容").gameObject.GetComponent<Text>().text = 
                1-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_1>=0?
                Convert.ToString(1-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_1) : Convert.ToString(0);

            // 获取技能2的图片
            bossSkill[1].transform.Find("布局").gameObject.GetComponent<Image>().sprite = Resources.Load(bossSkillPath+bossName+"2", typeof(Sprite)) as Sprite;
            bossSkill[1].transform.Find("布局").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);


            // 更新技能2的CD，直接用Mp_2和需要Mp的差值，这两个参数设定在Boss类下
            bossSkill[1].transform.Find("CD2").Find("内容").gameObject.GetComponent<Text>().text = 
                3-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_2>=0?
                Convert.ToString(3-boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.Mp_2) : Convert.ToString(0);
        }
    }
}
