using System.Collections.Generic;

namespace Genpai
{

    /// <summary>
    /// 管理普通游戏流程。
    /// 注意此处没有设置一个通用的游戏流程管理，因为此处暂时够用，且短期内新增流程概率不大，故不做过度设计。
    /// </summary>
    public class NormalProcessManager : MonoSingleton<NormalProcessManager>
    {
        /// <summary>
        /// 维护需要循环的处理流程
        /// </summary>
        private List<IProcess> _loopProcessList = new List<IProcess>();
        /// <summary>
        /// 当前正处于什么处理流程，值是int类型，数值是_loopProcessList数组下标的位置
        /// </summary>
        private int _currentProcess
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化流程管理，必须先调用，否则会报错
        /// </summary>
        public void Init()
        {
            _loopProcessList.Add(ProcessRoundStart.GetInstance());
            _loopProcessList.Add(ProcessRound.GetInstance());
            _loopProcessList.Add(ProcessRoundEnd.GetInstance());
            _loopProcessList.Add(ProcessBoss.GetInstance());
            _currentProcess = -1;
        }

        /// <summary>
        /// 开始本局游戏
        /// </summary>
        public void Start()
        {
            ProcessGameStart.GetInstance().Run();
        }

        /// <summary>
        /// 执行下一个流程
        /// </summary>
        public void Next()
        {
            if (_loopProcessList.Count - 1 == _currentProcess)
            {
                _currentProcess = 0;
                _loopProcessList[0].Run();
                return;
            }
            _currentProcess++; // 不要想着省一行写成_loopProcessList[++_currentProcess].Run(); 可能会被领导说。
            GetCurrentRound().Run();
        }

        private IProcess GetCurrentRound()
        {
            return _loopProcessList[_currentProcess];
        }

        /// <summary>
        /// 结束本局游戏
        /// </summary>
        public void End()
        {
            ProcessGameEnd.GetInstance().Run();
        }

        /// <summary>
        /// 结束本回合
        /// </summary>
        public void EndRound(GenpaiController genpaiController)
        {
            if (GetCurrentRound().GetName() != ProcessRound.NAME)
            {
                return;
            }
            if (GameContext.CurrentPlayer.GenpaiController != genpaiController)
            {
                return;
            }
            Next();
        }
    }
}