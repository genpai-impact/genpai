using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messager;

public class EndGameCheck :  MonoBehaviour,IMessageReceiveHandler
{
    static int Winner;
    private const int _bossWin = 1;
    private const int _charaWin = 2;
    
    private void Awake()
    {
        Subscribe();
    }

    public void Subscribe()
    {
        MessageManager.Instance.GetManager(MessageArea.Context)
            .Subscribe<bool>(MessageEvent.ContextEvent.BossFall, UseVicPage);
        MessageManager.Instance.GetManager(MessageArea.Context)
            .Subscribe<bool>(MessageEvent.ContextEvent.CharaFall, UseFailPage);
    }

    public void UseVicPage(bool _none)
    {
        Winner = _charaWin;
    }

    public void UseFailPage(bool _none)
    {
        Winner = _bossWin;
    }

    public static void CheckWinner()
    {
        if (Winner == _bossWin)
        {
            GameObject failEndGame = GameObject.Find("FailEndGame");
            failEndGame.SetActive(false);
        }
        else if (Winner == _charaWin)
        {
            GameObject vicEndGame = GameObject.Find("VicEndGame");
            vicEndGame.SetActive(false);
        }
        else
        {
            Debug.Log("玩家和boss都没有胜利！");
        }
    }




}
