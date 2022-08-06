using System.Collections.Generic;
using System.Net;
using cfg.level;
using DataScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;
using Utils.Messager;

namespace GameSystem.LevelSystem.EventSystem
{
    /// <summary>
    /// 以后再说吧现在懒得写
    /// </summary>
    public class EventController: MonoSingleton<EventController>
    {
        public int eventId;

        public int currentDialogId;

        private EventItem _eventItem;

        private EventDialogItem _eventDialogItem;
        
        private List<string> CurrentDialogList => _eventDialogItem.EventStory;

        private IEnumerator<string> _currentDialog;

        // --- 

        public GameObject dialogPrinter;
        
        public GameObject choiceLayout;

        public TextMeshProUGUI text;

        void Awake()
        {
            // 启动时隐藏
            dialogPrinter.SetActive(false);
            choiceLayout.SetActive(false);
        }

        public void Init()
        {
            dialogPrinter.SetActive(false);
            choiceLayout.SetActive(false);
        }
        
        public void Init(int id)
        {
            eventId = id;
            _eventItem = LubanLoader.GetTables().EventItems.Get(eventId);
            dialogPrinter.SetActive(true);

            InitDialog(_eventItem.EventDialogID);
        }

        private void InitDialog(int dialogId)
        {
            _eventDialogItem = LubanLoader.GetTables().EventDialogItems.GetOrDefault(dialogId);
            
            _currentDialog = CurrentDialogList.GetEnumerator();
            _currentDialog.MoveNext();
            
            PrintDialog();
        }

        private void PrintDialog()
        {
            text.text = _currentDialog.Current;
        }

        public void ChangeDialog()
        {
            // 下一句
            if (_currentDialog.MoveNext()) {PrintDialog();return;}
            
            // 如果没有下一句
            // 检查奖励
            if (_eventDialogItem.Rewards.Count != 0) GetReward();
            
            // 检查选项
            if(_eventDialogItem.SelectionStory.Count == 0) {AfterDialog(); return;}
            
            PrintChoice();
            
        }

        public void AfterDialog()
        {
            dialogPrinter.SetActive(false);
            // fixme
            //LevelUnlockChecker.UserUnlockers.Add(new LevelUnlocker(UnlockType.Event,currentDialogId,1));
            Debug.LogWarning("待恢复");
            
            MessageManager.Instance.Dispatch(MessageArea.Level,MessageEvent.LevelEvent.FreshUnlock,true);
        }

        /// <summary>
        /// 打印后续选项
        /// </summary>
        private void PrintChoice()
        {
            if (choiceLayout.activeSelf) return;

            var choiceList = _eventDialogItem.SelectionStory;

            GameObject buttonTemplate = Resources.Load<GameObject>("Prefabs/EventButton");
            choiceLayout.SetActive(true);
            
            foreach (var choice in choiceList)
            {
                if (LubanLoader.GetTables().EventDialogItems.GetOrDefault(choice) == default) return;
                // 如果为可选项则往Grid里输出

                GameObject button = Instantiate(buttonTemplate, choiceLayout.transform);
                button.GetComponent<EventChoiceButton>().Init(choice);
            }
        }


        /// <summary>
        /// 设定后续分支路径
        /// 由EventChoiceButton调用
        /// </summary>
        /// <param name="choiceId"></param>
        public void SelectChoice(int choiceId)
        {
            currentDialogId = choiceId;
            
            InitDialog(currentDialogId);
            

            ClearChilds(choiceLayout.transform);
            choiceLayout.SetActive(false);
            
            
            
        }
        
        /// <summary>
        /// 清除所有子物体
        /// </summary>
        private static void ClearChilds(Transform parent)
        {
            if (parent.childCount == 0) return;
            
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
            
        }
        
        /// <summary>
        /// 事件结束时分发奖励
        /// 暂时不用写
        /// </summary>
        private void GetReward()
        {
            foreach (var reward in _eventDialogItem.Rewards)
            {
                
            }
            
        }
    }
}