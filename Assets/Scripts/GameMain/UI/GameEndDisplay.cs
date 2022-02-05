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

        void IsBossFall(bool _none)
        {
            GameObject vicGameEnd = GameObject.Find("VicGameEnd");
            vicGameEnd.SetActive(true);
        }

        void IsPlayerFall(bool _none)
        {
            GameObject failGameEnd = GameObject.Find("FailGameEnd");
            failGameEnd.SetActive(true);
        }


    }
}

