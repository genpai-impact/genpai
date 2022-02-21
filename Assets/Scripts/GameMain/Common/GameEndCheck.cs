using System.Collections;
using System.Collections.Generic;
using Messager;
using UnityEngine;
using UnityEngine.UI;

public class GameEndCheck : MonoBehaviour,IMessageReceiveHandler
{
    public  GameObject vicEndGameObject;
    public  GameObject failEndGameObject;
    
    private void Awake()
    {
        Subscribe();
    }
    public void Subscribe()
    {
        MessageManager.Instance.GetManager(MessageArea.Context).Subscribe<bool>(MessageEvent.ContextEvent.BossFall, IsBossFall);
        MessageManager.Instance.GetManager(MessageArea.Context).Subscribe<bool>(MessageEvent.ContextEvent.CharaFall, IsPlayerFall);
    }

    public void IsBossFall(bool _none)
    {
        Debug.Log(("Chara Win!"));
        vicEndGameObject.SetActive(true);
    }

    public void IsPlayerFall(bool _none)
    {
        Debug.Log(("Chara Lose!"));
        failEndGameObject.SetActive(true);
    }
}
