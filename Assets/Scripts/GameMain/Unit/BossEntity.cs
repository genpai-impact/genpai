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
    public class BossEntity : UnitEntity, IMessageSendHandler
    {

        public new int HP
        {
            get => HP;
            set
            {

                if (HP > 0.75 * unit.HPMax && value <= 0.75 * unit.HPMax)
                {
                    MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach75, true);
                }
                if (HP > 0.5 * unit.HPMax && value <= 0.5 * unit.HPMax)
                {
                    MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.OnBossHPReach50, true);
                }
                HP = value;
            }
        }

        private void Awake()
        {
            // 从数据库获取技能等插件
        }

        /// <summary>
        /// 实现Boss死亡消息
        /// 覆盖父类SetFall
        /// </summary>
        public new void SetFall()
        {
            base.SetFall();
            MessageManager.Instance.Dispatch(MessageArea.Context, MessageEvent.ContextEvent.BossFall, true);
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            // 受伤时发送更新计分条消息，力竭时发送游戏结束消息
        }
    }
}