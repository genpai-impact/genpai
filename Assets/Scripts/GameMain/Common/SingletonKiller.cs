using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

namespace Genpai
{
    public class SingletonKiller : MonoBehaviour,IMessageReceiveHandler
    {
        /// <summary>
        /// 用于销毁战斗结束时，所有的单例
        /// </summary>
        private void Awake()
        {
            Subscribe();
        }
        public void Subscribe()
        { 
            //订阅消息
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<bool>(MessageEvent.ProcessEvent.OnGameRestart, KillMonoSingletonAll);
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<bool>(MessageEvent.ProcessEvent.OnGameRestart, KillSingletonAll);
            
        }
        
        /// <summary>
        /// 消灭Mono单例
        /// </summary>
        public void KillMonoSingletonAll(bool _none)
        {
            
            Debug.Log("Destory MonoSingletons.");
            UserLoader.Instance.Clean();
            ScoringBroad.Instance.Clean();
            PlayerLoader.Instance.Clean();
            BattleFieldManager.Instance.Clean();
            CardLoader.Instance.Clean();
            PrefabsLoader.Instance.Clean();
            
        }
        
        /// <summary>
        /// 消灭单例
        /// </summary>
        public void KillSingletonAll(bool _none)
        {
            
            Debug.Log("Destory Singletons.");
            AttackManager.Instance.Clean();
            DamageCalculator.Instance.Clean();
            EffectManager.Instance.Clean();
            GameContext.Instance.Clean();
            MessageManager.Instance.Clean();
            NormalProcessManager.Instance.Clean();
            SummonManager.Instance.Clean();
            
        }
    }
}

