using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    /// <summary>
    /// 统一效果管理器（伤害、恢复、Buff and so on）
    /// （单位受伤及下场是否由此统一管理？Boss等事件消息是否需要额外设立UnitManager进行管理？当前方案为由单位自行执行，不够合理）
    /// </summary>
    public class EffectManager : MonoSingleton<EffectManager>, IMessageHandler
    {
        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public void Execute(string eventCode, object message)
        {
            throw new System.NotImplementedException();
        }

        public void Subscribe()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}