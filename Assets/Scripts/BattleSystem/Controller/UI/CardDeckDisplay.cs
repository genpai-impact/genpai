using BattleSystem.Service.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleSystem.Controller.UI
{
    /// <summary>
    /// 实现牌库UI展示
    /// TODO：获取玩家卡牌数量
    /// </summary>
    public class CardDeckDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject InfoPanel;

        //实现牌堆厚度插件
        public GameObject[] ThickPanel;

        //面板最大数量为10
        private static int MaxPanelNum = 10;
        //实际牌数量与显示牌数量的比值
        private static int Divisor = 3;
        //当前面板数量
        private int NowPanelNum;

        //剩余卡牌数
        public int RemainCardNum;

        //延时设定
        public float DelayTime = 0.5f;
        private bool IsTimerSet = false;
        public void Start()
        {
            RemainCardNum = GameContext.GetCurrentPlayer().CardDeck.CardLibrary.Count;
            NowPanelNum = RemainCardNum > 2 ? RemainCardNum / Divisor : 1;
            for (int i = 0; i < NowPanelNum; i++)
            {
                ThickPanel[i].SetActive(true);
            }
        }

        /// <summary>
        /// 鼠标移入实现悬浮卡池余量
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            IsTimerSet = true;
            // 延时唤醒更新函数
            RemainCardNum = GameContext.GetCurrentPlayer().CardDeck.CardLibrary.Count;
            Invoke("UpdateInfo", DelayTime);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsTimerSet = false;
            InfoPanel.SetActive(false);
        }

        /// <summary>
        /// 更新牌库信息
        /// </summary>
        public void UpdateInfo()
        {
            if (IsTimerSet)
            {
                // RemainCardNum = GameContext.Instance.GetCurrentPlayer().CardDeck.GetRemainCard();
                string Text = "你的牌库中有" + RemainCardNum + "张牌";
                InfoPanel.GetComponent<Text>().text = Text;
                InfoPanel.SetActive(true);
            }
        }

        /// <summary>
        /// 更新卡牌厚度
        /// TODO: 在发牌的时候调用
        /// 暂定，根据美术素材调整
        /// </summary>
        public void UpdateThick()
        {
            //显示的卡牌数量为实际除Divisor,向下取整
            //少于3张时显示1张;
            int ShowPanelNum = RemainCardNum > 2 ? RemainCardNum / Divisor : 1;
            if (ShowPanelNum == NowPanelNum)
            {
                //更新后的显示面板数量与原值相同，无需进行操作
                return;
            }
            NowPanelNum = ShowPanelNum;

            for (int i = 0; i < ShowPanelNum; i++)
            {
                ThickPanel[i].SetActive(true);
            }
            for (int i = ShowPanelNum; i < MaxPanelNum; i++)
            {
                ThickPanel[i].SetActive(false);
            }
        }

        /// <summary>
        /// 最后一张牌显示特殊效果
        /// </summary>
        public void LastCardEffect()
        {
            
        }
    }
}