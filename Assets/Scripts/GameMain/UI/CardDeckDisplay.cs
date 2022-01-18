using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Genpai
{
    /// <summary>
    /// 实现牌库UI展示
    /// </summary>
    public class CardDeckDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject InfoPanel;
        public int RemainCardNum=10;//临时值，待修改
        //计时器
        private float timer = 0;
        //延时时间
        public float DelayTime = 0.5f;
        private bool IsStartTime = false;

        /// <summary>
        /// 鼠标移入实现悬浮卡池余量
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnPointerEnter(PointerEventData eventData)
        {
            //InfoPanel.SetActive(true);
            timer = 0;
            IsStartTime = true;
        }

        public void Update()
        {
            if (IsStartTime)
            {
                timer += Time.deltaTime;
                if (timer >= DelayTime)
                {
                    InfoPanel.SetActive(true);
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            timer = 0;
            IsStartTime = false;
            InfoPanel.SetActive(false);
        }

        /// <summary>
        /// 唤醒时绑定卡池
        /// </summary>
        private void Awake()
        {
            
        }
    }
}