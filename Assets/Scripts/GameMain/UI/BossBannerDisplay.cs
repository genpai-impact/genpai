using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// boss计分显示（待确定技能及Buff列表是否由此统一显示）
    /// 需求：https://www.teambition.com/project/61a89798beaeab07a42c799c/works/61c5cc58f516a2003f0cd9c4/work/61d99eaf6dff90003f3a3a8d
    /// </summary>
    public class BossBannerDisplay : MonoBehaviour, IMessageReceiveHandler
    {

        public void UpdateDisplay()
        {
            // 从游戏上下文中获取信息并更新
        }

        // 收到受击消息时更新banner显示
        public void Execute(int eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe()
        {
            // 向UI管理器订阅
        }

        /// <summary>
        /// 唤醒时绑定Boss信息及上下文
        /// </summary>
        private void Awake()
        {

        }

    }
}