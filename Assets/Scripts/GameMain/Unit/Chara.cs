﻿using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 角色类
    /// 需要实现出场技及主动技能，以及具有充能特性
    /// </summary>
    public class Chara : Unit, IMessageSendHandler
    {
        public Chara(UnitCard unitCard, PlayerID _owner) : base(unitCard, _owner)
        {
        }

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            // 角色力竭时发送消息更换角色
        }


    }
}