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
    /// 交互需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61cca0706eb9a5003fe3eb3a
    /// 展示需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d1598a9a6b6f003fdf2973
    /// 角色名片需求（待拆分）
    /// 待开发charaBanner脚本及prefab
    /// 名片需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61c9857eb079f7003fca4c72
    /// </summary>
    public class CardOnHand : MonoBehaviour, IPointerDownHandler, IMessageHandler
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

    }
}