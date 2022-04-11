using Messager;
using UnityEngine;

namespace Genpai
{
    class ProcessGameEnd : IProcess
    {
        private static ProcessGameEnd gameEndProcess = new ProcessGameEnd();
        private ProcessGameEnd()
        {
        }
        public static ProcessGameEnd GetInstance()
        {
            return gameEndProcess;
        }

        public void Dispatch(MessageArea areaCode, string eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            return "OnGameEnd";
        }
        

        public void Run()
        {
            //退出
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}