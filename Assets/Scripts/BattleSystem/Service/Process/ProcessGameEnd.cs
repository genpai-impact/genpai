using Utils.Messager;

namespace BattleSystem.Service.Process
{
    class ProcessGameEnd : IProcess
    {
        private static readonly ProcessGameEnd GameEndProcess = new ProcessGameEnd();
        private ProcessGameEnd()
        {
        }
        public static ProcessGameEnd GetInstance()
        {
            return GameEndProcess;
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
            #endif
        }
    }
}