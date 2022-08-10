using System.Collections.Generic;
using Utils;
using BattleSystem.Controller;
using UnityEngine.Events;
using System.Collections;
using BattleSystem.Service.Common;
using UnityEngine;
namespace BattleSystem.Service.Process
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
        private readonly List<IProcess> _loopProcessList = new List<IProcess>();
        /// <summary>
        /// 当前正处于什么处理流程，值是int类型，数值是_loopProcessList数组下标的位置
        /// </summary>
        private int CurrentProcess
        {
            get;
            set;
        }

        private bool _nextTrigger=true;
        /// <summary>
        /// 获取当前的流程
        /// </summary>
        /// <returns>当前流程</returns>
        public IProcess GetCurrentProcess()
        {
            return _loopProcessList[CurrentProcess];
        }

        private void Update()
        {
            if (AnimationHandle.Instance.AllAnimationOver() && AnimatorManager.Instance.NoAnimationInQuene() && _nextTrigger)
            {

                //if(CurrentProcess==3|| CurrentProcess==1)
                Debug.Log("当前进程："+CurrentProcess+ " 当前玩家："+GameContext.CurrentPlayer.playerName+"\n");
                _nextTrigger = false;
                GetCurrentProcess().Run();
                
            }
        }

        /// <summary>
        /// 初始化流程管理，必须先调用，否则会报错
        /// </summary>
        public void Init()
        {
            _loopProcessList.Add(ProcessBoss.GetInstance());
            _loopProcessList.Add(ProcessRoundStart.GetInstance());
            _loopProcessList.Add(ProcessRound.GetInstance());
            _loopProcessList.Add(ProcessRoundEnd.GetInstance());
            CurrentProcess = -1;
        }

        public void Fresh()
        {
            _loopProcessList.Clear();
        }

        /// <summary>
        /// 开始本局游戏,忘改名导致实例化两次，艹 2022/7/28
        /// </summary>
        public void StartProcess()
        {
            Init();
            ProcessGameStart.GetInstance().Run();
        }

        

        /// <summary>
        /// 执行下一个流程
        /// </summary>
        public void Next()
        {
            if (_loopProcessList.Count - 1 == CurrentProcess)
            {
                _nextTrigger = true;

                CurrentProcess = 0;
                //_loopProcessList[0].Run();
                return;
            }

            

            _nextTrigger = true;
            CurrentProcess++; // 不要想着省一行写成_loopProcessList[++_currentProcess].Run(); 可能会被领导说。

            
            //GetCurrentProcess().Run();
           
            
           
        }



        /// <summary>
        /// 结束本局游戏
        /// </summary>
        public void End()
        {
            ProcessGameEnd.GetInstance().Run();
        }

        /*/// <summary>
        /// 结束本回合，好像什么也没做？不是很明白之前咋跑起来的hh 真▪运行全靠bug 2022/7/28
        /// </summary>
        public void EndRound()
        {
            if (GetCurrentProcess().GetName() != ProcessRound.Name)
            {
                return;
            }
            
        }*/

        /// <summary>
        /// 结束本回合
        /// </summary>
        public void EndRound()
        {
            if (GetCurrentProcess().GetName() != ProcessRound.Name)
            {
                return;
            }
            Next();
        }

        

        /// <summary>
        /// 重开游戏
        /// </summary>
        public void Restart()
        { 
            ProcessGameRestart.GetInstance().Run();
        }
        
    }
}