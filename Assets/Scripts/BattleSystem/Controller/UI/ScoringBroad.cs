using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messager;
using UnityEngine.Serialization;

namespace Genpai
{
    public class ScoringBroad : MonoSingleton<ScoringBroad>, IMessageReceiveHandler
    {
        [FormerlySerializedAs("P1ScoreText")] public Text p1ScoreText;
        [FormerlySerializedAs("P2ScoreText")] public Text p2ScoreText;

        [FormerlySerializedAs("P1Score")] public int p1Score = 0;
        [FormerlySerializedAs("P2Score")] public int p2Score = 0;
        
        public void Init()
        {
            Subscribe();
            UpdateDisplay();
        }

        public void UpdateScore(BossScoringData data)
        {
            if (data.site == BattleSite.P1)
            {
                p1Score += data.score;
            }
            else
            {
                p2Score += data.score;
            }
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            p1ScoreText.text = p1Score.ToString();
            p2ScoreText.text = p2Score.ToString();
        }

        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Context)
                .Subscribe<BossScoringData>(MessageEvent.ContextEvent.BossScoring, UpdateScore);
        }
        
    }
}