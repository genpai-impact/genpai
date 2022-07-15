using UnityEngine;
using UnityEngine.UI;
using Utils.Messager;

namespace BattleSystem.Service.Common
{
    public class GameEndCheck : MonoBehaviour,IMessageReceiveHandler
    {
        public  GameObject vicEndGameObject;
        public  GameObject failEndGameObject;

        public GameObject BarUI;

        private bool vic = false;
        private bool fail = false;
    
        private void Awake()
        {
            Subscribe();
        }

        public void Subscribe()
        {
            //订阅消息
            MessageManager.Instance.GetManager(MessageArea.Context).Subscribe<bool>(MessageEvent.ContextEvent.BossFall, IsBossFall);
            MessageManager.Instance.GetManager(MessageArea.Context).Subscribe<bool>(MessageEvent.ContextEvent.CharaFall, IsPlayerFall);
        }


        // 展示结束界面的条件有两个：
        // 1. 决出胜负
        // 2. BOSS展示的血条归零
        // 分出这两个条件的主要原因还是因为项目中，伤害计算和动画展示是两个环节，所以会有一些错位。为此用了两个判定条件
        // 2022/7/15 凹凸曼
        private void Update() {
            if(vic && BarUI.GetComponent<Image>().fillAmount == 0)
            {
                vicEndGameObject.SetActive(true);
            }

            if(fail && BarUI.GetComponent<Image>().fillAmount == 0)
            {
                failEndGameObject.SetActive(true);
            }
        }
    
        /// <summary>
        /// 玩家获胜！
        /// </summary>
        /// <param name="_none"></param>
        public void IsBossFall(bool _none)
        {
            Debug.Log(("Chara Win!"));

            // vicEndGameObject.SetActive(true);
            vic = true;
        }

        /// <summary>
        /// 玩家落败！
        /// </summary>
        /// <param name="_none"></param>
        public void IsPlayerFall(bool _none)
        {
            Debug.Log(("Chara Lose!"));

            // failEndGameObject.SetActive(true);
            fail = true;
        }
    
    }
}
