using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Messager;


namespace Genpai
{
    /// <summary>
    /// 卡牌管理器
    /// </summary>
    public class HandCardManager : MonoSingleton<HandCardManager>
    {

        public List<GameObject> handCards;

        public GenpaiPlayer waitingPlayer;
    }
}