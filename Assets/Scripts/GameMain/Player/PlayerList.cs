using System.Collections;
using System.Collections.Generic;
using Genpai;
using UnityEngine;
using UnityEngine.UI;
using Messager;
using UnityEngine.PlayerLoop;

/// <summary>
/// 用来储存游戏中玩家的战斗信息（玩家分数，回合数，等等，未来按照需求设计）
/// </summary>

public class PlayerList:MonoSingleton<PlayerList>,IMessageReceiveHandler
{
    public Text P1ScoreText;
    public Text P2ScoreText;

    public Text RoundNumberText;


    public int P1Score = 0;
    public int P2Score = 0;
    public int RoundNumber = 0;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {

        Subscribe();
        EndScoreDisplay();
        
    }

    /// <summary>
    /// 游戏结束时统计玩家分数的方法
    /// </summary>
    /// <param name="data"></param>
    public void PlayerScore(BossScoringData data)
    {
        if (data.site == BattleSite.P1)
        {
            P1Score += data.score;
        }
        else
        {
            P2Score += data.score;
        }

        EndScoreDisplay();
    }

    /// <summary>
    /// 游戏结束时统计游戏回合数的方法
    /// </summary>
    public void RoundCount()
    {
        
    }
    
    
    public void EndScoreDisplay()
    {
        P1ScoreText.text = P1Score.ToString();
        P2ScoreText.text = P2Score.ToString();
    }
    
    


    public void Subscribe()
    {
        MessageManager.Instance.GetManager(MessageArea.Context)
            .Subscribe<BossScoringData>(MessageEvent.ContextEvent.BossScoring, PlayerScore);
        
        // MessageManager.Instance.GetManager(MessageArea.Process)
        //     .Subscribe<BossScoringData>(MessageEvent.ContextEvent.BossScoring, RoundCount);
    }

}
