using Messager;

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

        public void Dispatch(MessageArea areaCode, int eventCode, object message)
        {
            MessageManager.Instance.Dispatch(areaCode, eventCode, message);
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
