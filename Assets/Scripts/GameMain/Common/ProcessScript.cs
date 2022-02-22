﻿
using Messager;
using UnityEngine;


namespace Genpai
{
    public class ProcessScript : MonoBehaviour
    {
        public void EndRound()
        {
            // Debug.Log("click");

            MessageManager.Instance.Dispatch(MessageArea.UI, MessageEvent.UIEvent.ShutUpHighLight, true);
            GameContext.CurrentPlayer.GenpaiController.EndRound();
        }

        public void EndGame()
        {
            
            Debug.Log("GameEnd");
            NormalProcessManager.Instance.End();
        }

        public void Restart()
        {
            
            Debug.Log(("Game Restart"));
            NormalProcessManager.Instance.Restart();
        }
    }
}
