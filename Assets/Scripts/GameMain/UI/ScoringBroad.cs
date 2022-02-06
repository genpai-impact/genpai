using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messager;

namespace Genpai
{
    public class ScoringBroad : MonoSingleton<ScoringBroad>, IMessageReceiveHandler
    {
        public Text P1ScoreText;
        public Text P2ScoreText;

        public int P1Score = 0;
        public int P2Score = 0;

        public void Init()
        {
            Subscribe();
            UpdateDisplay();
        }

        public void UpdateScore(BossScoringData data)
        {
            if (data.site == BattleSite.P1)
            {
                P1Score += data.score;
            }
            else
            {
                P2Score += data.score;
            }
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            P1ScoreText.text = P1Score.ToString();
            P2ScoreText.text = P2Score.ToString();
        }

        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Context)
                .Subscribe<BossScoringData>(MessageEvent.ContextEvent.BossScoring, UpdateScore);
        }
    }
}