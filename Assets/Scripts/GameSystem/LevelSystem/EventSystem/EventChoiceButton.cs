using DataScripts;
using TMPro;
using UnityEngine;

namespace GameSystem.LevelSystem.EventSystem
{
    public class EventChoiceButton : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private int _choiceId;

        public void Init(int id)
        {
            _choiceId = id;
        
            var item = LubanLoader.GetTables().EventDialogItems.GetOrDefault(id);
        
            text.text = item.DialogName;
        
        }

        /// <summary>
        /// 选择时接口
        /// </summary>
        public void ButtonDown()
        {
            EventController.Instance.SelectChoice(_choiceId);
        }
    
    }
}
