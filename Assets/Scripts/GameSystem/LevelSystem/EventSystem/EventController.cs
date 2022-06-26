using System.Collections.Generic;
using cfg.level;
using DataScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace GameSystem.LevelSystem.EventSystem
{
    /// <summary>
    /// 以后再说吧现在懒得写
    /// </summary>
    public class EventController: MonoSingleton<EventController>
    {
        public int EventId;

        public int CurrentDialogId;

        private EventItem _eventItem;

        private EventDialogItem _eventDialogItem;

        public GridLayoutGroup choiceLayout;

        public TextMeshProUGUI text;

        void Awake()
        {
            // 启动时隐藏
            gameObject.SetActive(false);
            choiceLayout.gameObject.SetActive(false);
        }

        public void Init(int eventId)
        {
            EventId = eventId;
            _eventItem = LubanLoader.GetTables().EventItems.Get(EventId);
            gameObject.SetActive(true);
        }

        private void PrintChoice()
        {
            var choiceList = _eventDialogItem.SelectionStory;
            if (choiceList.Count == 0) return;
            
            GameObject buttonTemplate = Resources.Load<GameObject>("Prefabs/EventButton");
            choiceLayout.gameObject.SetActive(true);
            
            foreach (var choice in choiceList)
            {
                if (LubanLoader.GetTables().EventDialogItems.GetOrDefault(choice) == default) return;
                // 如果为可选项则往Grid里输出

                GameObject button = GameObject.Instantiate(buttonTemplate, choiceLayout.transform);
                button.GetComponent<EventChoiceButton>().Init(choice);
            }
        }

        
        
        /// <summary>
        /// 设定后续分支路径
        /// 由EventChoiceButton调用
        /// </summary>
        /// <param name="choiceId"></param>
        public void SetChoice(int choiceId)
        {
            CurrentDialogId = choiceId;
            _eventDialogItem = LubanLoader.GetTables().EventDialogItems.Get(CurrentDialogId);

            ClearChilds(choiceLayout.transform);
            choiceLayout.gameObject.SetActive(false);
            
        }
        
        /// <summary>
        /// 清除所有子物体
        /// </summary>
        private void ClearChilds(Transform parent)
        {
            if (parent.childCount == 0) return;
            
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
            
        }
        
        /// <summary>
        /// 事件结束时分发奖励
        /// </summary>
        private void GetReward()
        {
            foreach (var reward in _eventDialogItem.Rewards)
            {
                
            }
            
        }
    }
}