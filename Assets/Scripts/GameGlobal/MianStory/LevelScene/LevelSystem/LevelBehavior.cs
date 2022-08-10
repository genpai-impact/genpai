using System;
using DataScripts;
using GameSystem.LevelSystem;
using GameSystem.LevelSystem.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils.Messager;



    /// <summary>
    /// 关卡按钮挂载本脚本，控制显示及后续流程
    /// </summary>
    public  class LevelBehavior 
    {
 
    
        //public Image bottom;
        //public Text text;

        //private bool unlocked;

        /// <summary>
        /// True: 标识关卡
        /// False: 标识事件
        /// </summary>


        /*private void Awake()
        {
            GetName();
            FreshUnlock();
           
        }*/

        /*public void GetName()
        {
            text.text = levelOrEvent ? 
                LubanLoader.GetTables().LevelItems.GetOrDefault(levelId).LevelName : 
                LubanLoader.GetTables().EventItems.GetOrDefault(levelId).EventName;
        }*/

        /*private void FreshUnlock(bool none = true)
        {
            Debug.Log("freshing");
            //unlocked = LevelUnlockChecker.CheckUnlock(levelId, levelOrEvent);
            ShowBottom();
        }*/

        /*private void ShowBottom()
        {
            bottom.color = new Color(1f, 1f, 1f, unlocked ? 1f : 0.5f);
        }*/

        public static void ClickChoice(int levelId,bool levelOrEvent)
        {
            //if(!unlocked) return;

            if (levelOrEvent)
            {
                LevelChoice(levelId);
            }
            else
            {
                EventChoice(levelId);
            }
            
        }

        private static void LevelChoice(int levelId)
        {
            // 设置关卡
            LevelInfoDontDestroy.Instance.levelId = levelId;
            
            // TODO: 设置卡组

            // 读取剧情
            string targetScene = LubanLoader.GetTables().LevelItems.Get(levelId).Story1;
            SceneManager.LoadScene(targetScene);
        }

        private static void EventChoice(int levelId)
        {
        
            // 要干啥捏
            EventController.Instance.Init();
            EventController.Instance.Init(levelId);
        }

       
    }
