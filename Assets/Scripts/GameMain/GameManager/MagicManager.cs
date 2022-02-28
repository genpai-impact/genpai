using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 技能管理器，受理技能攻击请求
    /// 现在给魔法卡使用，参考AttackManager实现
    /// </summary>
    class MagicManager : Singleton<MagicManager>, IMessageHandler
    {
        /// <summary>
        /// 请求攻击玩家
        /// </summary>
        public BattleSite waitingPlayer;

        /// <summary>
        /// 当前是否处于等待
        /// </summary>
        public bool eventWaiting;


        private MagicManager()
        {
            Subscribe();
            eventWaiting = false;
        }

        public void Init()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sourceUnit"></param>
        void AttackRequest((UnitEntity, GameObject) arg)
        {
            Debug.Log("Magic Attack Request");
            MessageManager.Instance.Dispatch(MessageArea.Attack, MessageEvent.AttackEvent.MagicAttackRequest, arg);
        }


        public void Dispatch(MessageArea areaCode, string eventCode, object message = null)
        {
        }

        public void Subscribe()
        {
            // 订阅单位发布的魔法攻击请求消息
            MessageManager.Instance.GetManager(MessageArea.Magic)
                .Subscribe<(UnitEntity, GameObject)>(MessageEvent.MagicEvent.AttackRequest, AttackRequest);

        }
    }
}
