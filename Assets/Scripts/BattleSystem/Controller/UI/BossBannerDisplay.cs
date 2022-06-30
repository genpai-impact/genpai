using UnityEngine;
using Utils.Messager;

namespace BattleSystem.Controller.UI
{
    /// <summary>
    /// boss计分显示（待确定技能及Buff列表是否由此统一显示）
    /// </summary>
    public class BossBannerDisplay : MonoBehaviour, IMessageReceiveHandler
    {

        public void UpdateDisplay()
        {
            // 从游戏上下文中获取信息并更新
        }

        // 收到受击消息时更新banner显示
        public void Execute(string eventCode, object message)
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