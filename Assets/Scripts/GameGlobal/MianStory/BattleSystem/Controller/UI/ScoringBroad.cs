using BattleSystem.Service.MessageDatas;
using BattleSystem.Service.Player;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;
using Utils.Messager;

namespace BattleSystem.Controller.UI
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

        // 只是为了在GameContextScript中进行新游戏的fresh的时候保持形式同一，没有特殊作用
        public void Fresh()
        {

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