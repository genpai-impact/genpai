using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// Boss类
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d97da5e8d5a0003fbaa446
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