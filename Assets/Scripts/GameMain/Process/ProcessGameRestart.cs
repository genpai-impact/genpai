using System.Collections;
using System.Collections.Generic;
using Messager;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Genpai
{
    public class ProcessGameRestart : IProcess
    {
        private static ProcessGameRestart gameRestart = new ProcessGameRestart();
        private ProcessGameRestart()
        {
        }
        public static ProcessGameRestart GetInstance()
        {
            return gameRestart;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            return "OnGameRestart";
        }

        public void Run()
        {
            MessageManager.Instance.Dispatch(MessageArea.Process, MessageEvent.ProcessEvent.OnGameRestart, true);
            SceneManager.LoadScene(0);
        }
        
    }
}

