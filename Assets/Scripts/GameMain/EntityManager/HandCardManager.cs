using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Messager;


namespace Genpai
{
    /// <summary>
    /// 手牌管理器
    /// TODO：将CardDeck部分操作转移实现
    /// </summary>
    public class HandCardManager : MonoSingleton<HandCardManager>
    {

        public List<GameObject> handCards;

        public GenpaiPlayer waitingPlayer;
    }
}