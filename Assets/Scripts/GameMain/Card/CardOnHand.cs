using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 卡牌于手牌中时行为层，绑定Prefab
    /// </summary>
    public class CardOnHand : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {
        public GenpaiPlayer player;
        public Image outLayer;

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public void Execute(string eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // 待增加回合判定
            if (GetComponent<CardDisplay>().card is UnitCard)
            {
                // Dispatch(MessageArea.Behavior, 0, gameObject);
            }
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }

    }
}