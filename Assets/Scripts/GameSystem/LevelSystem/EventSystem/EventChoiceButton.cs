using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using cfg.level;
using DataScripts;
using GameSystem.LevelSystem.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        EventController.Instance.SetChoice(_choiceId);
    }
    
}
