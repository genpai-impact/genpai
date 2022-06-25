using UnityEngine.SceneManagement;
using Utils.Messager;

namespace BattleSystem.Service.Process
{
    public class ProcessGameRestart : IProcess
    {
        private static readonly ProcessGameRestart GameRestart = new ProcessGameRestart();
        private ProcessGameRestart()
        {
        }
        public static ProcessGameRestart GetInstance()
        {
            return GameRestart;
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

