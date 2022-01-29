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
        public Chara chara;

        /// <summary>
        /// 当前能量值
        /// </summary>
        public int MP
        {
            get => chara.MP;
            set => chara.MP = value;
        }
        private void Awake()
        {
            // 待实现：从数据库获取技能、充能等信息

            //Subscribe();
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

        /// <summary>
        /// 覆盖父类的Subscribe方法,
        /// 比父类增加了充能的订阅,
        /// 这个大概能在父类UnitEntity的AWAKE()中被调用吧。。。。
        /// </summary>
        public new void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, AddMP);  // 把充一点MP这件事情添加到新回合开始时要做的事情中
        }
        /// <summary>
        /// 充一点能量，如果满了就不充
        /// </summary>
        public void AddMP(bool _none)
        {
            if(0 <= MP && MP <= 3)
            {
                MP++;
            }
        }
    }
}