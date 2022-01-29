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

        public new int HP
        {
            get => HP;
            set
            {

                if (HP > 0.75 * HPMax && value <= 0.75 * HPMax)
                {

                }
                if (HP > 0.5 * HPMax && value <= 0.5 * HPMax)
                {

                }
                HP = value;
            }
        }
        public Boss(UnitCard unitCard) : base(unitCard)
        {
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            // 受伤时发送更新计分条消息，力竭时发送游戏结束消息
        }
    }
}