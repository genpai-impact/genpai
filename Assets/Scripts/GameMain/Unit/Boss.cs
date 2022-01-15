using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Boss类
    /// 有不反击等特性及技能组
    /// </summary>
    public class Boss : Unit, IMessageSendHandler
    {

        public Boss(UnitCard unitCard, PlayerID _owner) : base(unitCard, _owner)
        {
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            // 受伤时发送更新计分条消息，力竭时发送游戏结束消息
        }
    }
}