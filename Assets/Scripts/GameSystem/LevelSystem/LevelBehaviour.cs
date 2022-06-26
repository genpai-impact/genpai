using System;
using DataScripts;
using GameSystem.LevelSystem.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils.Messager;

namespace GameSystem.LevelSystem
{
    /// <summary>
    /// 关卡按钮挂载本脚本，控制显示及后续流程
    /// </summary>
    public class LevelBehaviour : MonoBehaviour, IMessageReceiveHandler
    {
        public int levelId;

        public Image bottom;
        public Text text;

        private bool unlocked;

        /// <summary>
        /// True: 标识关卡
        /// False: 标识事件
        /// </summary>
        public bool levelOrEvent;

        private void Awake()
        {
            text.text = LubanLoader.GetTables().LevelItems.Get(levelId).LevelName;
            FreshUnlock();
            Subscribe();
        }

        private void FreshUnlock(bool none = true)
        {
            unlocked = LevelUnlockChecker.CheckUnlock(levelId);
            ShowBottom();
        }

        private void ShowBottom()
        {
            bottom.color = new Color(1f, 1f, 1f, unlocked ? 1f : 0.5f);
        }

        public void Choice()
        {
            if(!unlocked) return;

            if (levelOrEvent)
            {
                LevelChoice();
            }
            else
            {
                EventChoice();
            }
            
        }

        private void LevelChoice()
        {
            // 设置关卡
            LevelInfoDontDestroy.Instance.levelId = levelId;
            
            // TODO: 设置卡组

            // 读取剧情
            string targetScene = LubanLoader.GetTables().LevelItems.Get(levelId).Story1;
            SceneManager.LoadScene(targetScene);
        }

        private void EventChoice()
        {
            // 要干啥捏
            EventController.Instance.Init(levelId);
        }

        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Level).Subscribe<bool>(MessageEvent.LevelEvent.FreshUnlock, FreshUnlock);
        }
    }
}