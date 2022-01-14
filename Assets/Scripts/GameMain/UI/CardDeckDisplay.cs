using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Genpai
{
    /// <summary>
    /// 实现牌库UI展示
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d94a495d25f1003f1aa8dd
    /// </summary>
    public class CardDeckDisplay : MonoBehaviour, IPointerEnterHandler
    {
        public CardDeck cardDeck;

        /// <summary>
        /// 鼠标移入实现悬浮卡池余量
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 唤醒时绑定卡池
        /// </summary>
        private void Awake()
        {

        }
    }
}