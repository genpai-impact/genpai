using System.Collections.Generic;

namespace Genpai
{

    /// <summary>
    /// ������ͨ��Ϸ���̡�
    /// ע��˴�û������һ��ͨ�õ���Ϸ���̹�����Ϊ�˴���ʱ���ã��Ҷ������������̸��ʲ��󣬹ʲ���������ơ�
    /// </summary>
    public class NormalProcessManager
    {
        private static NormalProcessManager normalProcessManager = new NormalProcessManager();
        private NormalProcessManager()
        {
        }
        public static NormalProcessManager GetInstance()
        {
            return normalProcessManager;
        }

        /// <summary>
        /// ά����Ҫѭ���Ĵ�������
        /// </summary>
        private List<IProcess> _loopProcessList = new List<IProcess>();
        /// <summary>
        /// ��ǰ������ʲô�������̣�ֵ��int���ͣ���ֵ��_loopProcessList�����±��λ��
        /// </summary>
        private int _currentProcess
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʼ�����̹��������ȵ��ã�����ᱨ��
        /// </summary>
        public void Init()
        {
            _loopProcessList.Add(BossProcess.GetInstance());
            _loopProcessList.Add(RoundStartProcess.GetInstance());
            _loopProcessList.Add(RoundProcess.GetInstance());
            _loopProcessList.Add(RoundEndProcess.GetInstance());
            _loopProcessList.Add(BossProcess.GetInstance());
            _loopProcessList.Add(RoundStartProcess.GetInstance());
            _loopProcessList.Add(RoundProcess.GetInstance());
            _loopProcessList.Add(RoundEndProcess.GetInstance());
            _loopProcessList.Add(RoundCounterProcess.GetInstance());
        }

        /// <summary>
        /// ��ʼ������Ϸ
        /// </summary>
        public void Start()
        {
            GameStartProcess.GetInstance().Run();
        }

        /// <summary>
        /// ִ����һ������
        /// </summary>
        public void Next()
        {
            if (_loopProcessList.Count - 1 == _currentProcess)
            {
                _currentProcess = 0;
                _loopProcessList[0].Run();
                return;
            }
            _currentProcess++; // ��Ҫ����ʡһ��д��_loopProcessList[++_currentProcess].Run(); ���ܻᱻ�쵼˵��
            _loopProcessList[_currentProcess].Run();
        }

        /// <summary>
        /// ����������Ϸ
        /// </summary>
        public void End()
        {
            GameEndProcess.GetInstance().Run();
        }

        /// <summary>
        /// �������غ�
        /// </summary>
        public void EndRound()
        {
            GameContext.IsOperable = false;
            Next();
        }
    }
}