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
    /// 日后的Boss生成过程大概是从数据库里随机一个boss信息，在Awake里传进来
    /// 个人认为是没有必要单独搞一个Boss.cs的
    public class BossEntity : UnitEntity, IMessageSendHandler
    {
        public Boss boss;


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

        /// <summary>
        /// 强制ATK获取为0
        /// </summary>
        public new int ATK
        {
            get => 0;
        }

        public int MPMax_1
        {
            get { return boss.MPMax_1; }
        }
        public int MPMax_2
        {
            get { return boss.MPMax_2; }
        }
        public int MP_1
        {
            get => boss.MP_1;
            set { boss.MP_1 = value; }
        }
        public int MP_2
        {
            get => boss.MP_2;
            set { boss.MP_2 = value; }
        }

        public override void Awake()
        {
            // 从数据库获取技能等插件
            base.Awake();
            this.Subscribe();
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

        public new void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, FreshActionState);
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<bool>(MessageEvent.ProcessEvent.OnRoundStart, this.AddMP);  // 把充一点MP这件事情添加到新回合开始时要做的事情中
        }

        public void AddMP(bool _none)
        {
            if (0 <= MP_1 && MP_1 < MPMax_1)
            {
                MP_1++;
            }
            if (0 <= MP_2 && MP_2 < MPMax_2)
            {
                MP_2++;
            }
        }
    }
}