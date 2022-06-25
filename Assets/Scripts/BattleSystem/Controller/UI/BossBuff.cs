using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BattleSystem.Service.Common;
using BattleSystem.Controller.Unit;
using BattleSystem.Controller.Unit.UnitView;

namespace BattleSystem.Controller.UI
{
    public class BossBuff : MonoBehaviour
    {   
        public GameObject[] bossBuff = new GameObject[7];
        public GameObject boss;

        private const int maxBuffLength = 7;
        private const string uiBuffPath = "ArtAssets/UI/战斗界面/Buff/";

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
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
