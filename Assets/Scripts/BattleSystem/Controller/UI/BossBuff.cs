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
    /// 添加boss的buff显示。作为component挂在BattleScene的 UI/UI布局-ALL/Boss相关/Buff 下
    /// 主要存在的问题：
    /// 在数据处理中并没有把buff比较好的分出来，比如 元素附着看起来应该是buff，但并没有写入UnitView的buffView变量中
    /// 目前添加的buff只有冻结。感电能识别到 但是没有图片，而且现在感电似乎也不算是一种buff了，我会再看下需求文档or找需求人员确认一下
    /// 2022/6/25 凹凸曼
    /// </summary>
    public class BossBuff : MonoBehaviour
    {   
        // 按照需求，确认最多只显示7个buff
        public GameObject[] bossBuff = new GameObject[7];

        // 场中boss
        public GameObject boss;

        // 最大buff显示数量
        private const int maxBuffLength = 7;

        // 资源路径
        private const string uiBuffPath = "ArtAssets/UI/战斗界面/Buff/";

        // 显示当前boss身上的buff，目前在UnitView类中只把 冻结 和 感电 看成buff并加入BuffView中，晚点再确认一下
        void Update()
        {
            List<BuffView> bossBuffView = boss.transform.Find("Unit").GetChild(0).gameObject.GetComponent<UnitEntity>().unitDisplay.UnitView.BuffViews;
            int cnt=0;

            foreach(BuffView buff in bossBuffView){
                if(cnt>=7) break;

                bossBuff[cnt].SetActive(true);
                
                string buffPath = uiBuffPath + buff.BuffName;

                bossBuff[cnt].transform.Find("布局").gameObject.GetComponent<Image>().sprite = Resources.Load(buffPath, typeof(Sprite)) as Sprite;
                bossBuff[cnt].transform.Find("布局").gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                Debug.Log(buffPath);

                cnt++;
            }

            for(; cnt<maxBuffLength; ++cnt){
                bossBuff[cnt].SetActive(false);
            }
        }
    }
}
