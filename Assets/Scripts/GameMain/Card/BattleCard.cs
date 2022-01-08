using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Messager;

namespace Genpai
{
    public class BattleCard : MonoBehaviour, IPointerDownHandler, IMessageHandler
    {
        public PlayerID player;
        public Image outLayer;

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public void Execute(int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // 待增加回合判定
            if (GetComponent<CardDisplay>().card is UnitCard)
            {
                Dispatch(MessageArea.Behavior, 0, gameObject);
            }
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}