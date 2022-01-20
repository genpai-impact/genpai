using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// 实现牌库UI展示
    /// TODO
    /// 需要拿到牌库剩余卡牌，目前测试默认为10
    /// </summary>
    public class CardDeckDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject InfoPanel;
        //实现厚度的10个面板
        public GameObject[] ThickPanel;
        //面板最大数量为10
        int MaxPanelNum = 10;
        //当前面板数量
        int NowPanelNum;
        //实际牌数量与显示牌数量的比值
        int Divisor = 3;
        //剩余卡牌数
        public int RemainCardNum;
        //计时器
        private float Timer = 0;
        //延时时间
        public float DelayTime = 0.5f;
        private bool IsStartTime = false;
        public void Start()
        {
            //待修改 可获得余牌数量后使用注释中的语句
            //RemainCardNum = GameContext.Player1.CardDeck.CardLibrary.Count;
            RemainCardNum = 10;
            NowPanelNum = RemainCardNum > 2 ? RemainCardNum / Divisor : 1;
            for(int i=0;i<NowPanelNum;i++)
            {
                ThickPanel[i].SetActive(true);
            }
        }

        /// <summary>
        /// 鼠标移入实现悬浮卡池余量
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnPointerEnter(PointerEventData eventData)
        {
            Timer = 0;
            IsStartTime = true;
        }

        public void Update()
        {
            //待修改 可获得余牌数量后使用注释中的语句
            //RemainCardNum = GameContext.Player1.CardDeck.CardLibrary.Count;
            UpdateInfo();
            UpdateThick();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Timer = 0;
            IsStartTime = false;
            InfoPanel.SetActive(false);
        }

        /// <summary>
        /// 更新牌库信息
        /// </summary>
        public void UpdateInfo()
        {
            if (IsStartTime)
            {
                Timer += Time.deltaTime;
                if (Timer >= DelayTime)
                {
                    string Text = "你的牌库中有" + RemainCardNum + "张牌";
                    InfoPanel.GetComponent<Text>().text = Text;
                    InfoPanel.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 更新卡牌厚度
        /// </summary>
        public void UpdateThick()
        {
            //显示的卡牌数量为实际除Divisor,向下取整
            //少于3张时显示1张;
            int ShowPanelNum=RemainCardNum>2?RemainCardNum/ Divisor : 1;
            if(ShowPanelNum==NowPanelNum)
            {
                //更新后的显示面板数量与原值相同，无需进行操作
                return;
            }
            NowPanelNum = ShowPanelNum;

            for(int i=0;i<ShowPanelNum;i++)
            {
                ThickPanel[i].SetActive(true);
            }
            for(int i=ShowPanelNum;i<MaxPanelNum;i++)
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

        /// <summary>
        /// 唤醒时绑定卡池
        /// </summary>
        private void Awake()
        {
            
        }
    }
}