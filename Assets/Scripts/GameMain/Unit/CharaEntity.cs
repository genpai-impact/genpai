using Messager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Genpai
{
    /// <summary>
    /// 角色实体
    /// 需要实现出场技及主动技能，以及充能特性
    /// </summary>
    public class CharaEntity : UnitEntity, IMessageSendHandler
    {

        private void Awake()
        {
            // 从数据库获取技能、充能等信息
        }

        /// <summary>
        /// 实现角色死亡消息
        /// 覆盖父类SetFall
        /// </summary>
        public new void SetFall()
        {
            base.SetFall();
            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {

        }



    }
}