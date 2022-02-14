using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;
namespace Genpai
{
    public class GameEndDisplay : MonoBehaviour, IMessageReceiveHandler
    {
        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<bool>(MessageEvent.ContextEvent.BossFall, IsBossFall);
            MessageManager.Instance.GetManager(MessageArea.Process).Subscribe<bool>(MessageEvent.ContextEvent.CharaFall, IsPlayerFall);
        }

        private void Awake()
        {
            Subscribe();
            
        }

        public static void IsBossFall(bool _none)
        {
            GameObject vicEndGame = GameObject.Find("VicEndGame");
            vicEndGame.SetActive(true);
        }

        public static void IsPlayerFall(bool _none)
        {
            GameObject failEndGame = GameObject.Find("FailEndGame");
            failEndGame.SetActive(true);
        }


    }
}

