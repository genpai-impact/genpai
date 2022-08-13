using BattleSystem.Service.MessageDatas;
using BattleSystem.Service.Player;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Utils.Messager;
using Utils;
using BattleSystem.Service;

namespace BattleSystem.Controller.UI
{
    /// <summary>
    /// 结束游戏面板控件
    /// </summary>
    public class EndGameBannerDisplay : MonoSingleton<EndGameBannerDisplay>, IMessageReceiveHandler
    {
        [Tooltip("己方伤害量")]
        [FormerlySerializedAs("LeftScoreText")] public Text leftScoreText;
        [Tooltip("总伤害量")]
        [FormerlySerializedAs("TotalScoreText")] public Text totalScoreText;
        [Tooltip("己方死亡数")]
        [FormerlySerializedAs("DeathsText")] public Text deathsText;
        [Tooltip("己方回合数")]
        [FormerlySerializedAs("RoundNumberText")] public Text roundNumberText;
        [Tooltip("伤害量比值")]
        [FormerlySerializedAs("DamegebarImage")] public Image damegebarImage;

        [FormerlySerializedAs("LeftScore")] public int leftScore=0;
        [FormerlySerializedAs("TotalScore")] public int totalScore=0;
        [FormerlySerializedAs("Deaths")] public int deaths=0;
        [FormerlySerializedAs("RoundNumber")] public int roundNumber=0;

        /// <summary>
        /// 初始化面板
        /// </summary>
        public void Init()
        {
            leftScore = 0;
            totalScore = 0;
            deaths = 0;
            roundNumber = 0;
            Subscribe();
            UpdateDisplay();
        }
        /// <summary>
        /// 更新数据面板
        /// </summary>
        public void UpdateDisplay()
        {
            leftScoreText.text = leftScore.ToString();
            totalScoreText.text = totalScore.ToString();
            deathsText.text = deaths.ToString();
            roundNumberText.text = roundNumber.ToString();
            damegebarImage.fillAmount = (float)(1.0 * leftScore / totalScore);
        }
        /// <summary>
        /// 更新伤害量
        /// </summary>
        /// <param name="data"></param>
        public void UpdateScore(BossScoringData data)
        {
            if (data.site == BattleSite.P1)
            {
                leftScore += data.score;
            }
            totalScore += data.score;
            UpdateDisplay();
        }
        /// <summary>
        /// 更新回合数
        /// </summary>
        /// <param name="site"></param>
        //回合数可能会有些异常增加
        public void UpdateRoundNumber(BattleSite site)
        {
            if (site == BattleSite.P1) roundNumber++;
            UpdateDisplay();
        }
        /// <summary>
        /// 更新死亡数
        /// </summary>
        /// <param name="site"></param>
        public void UpdateDeaths(BattleSite site)
        {
            if (site == BattleSite.P1) deaths++;
            UpdateDisplay();
        }
        /// <summary>
        /// Message订阅。用于获取游戏信息。
        /// </summary>
        public void Subscribe()
        {
            MessageManager.Instance.GetManager(MessageArea.Context)
                .Subscribe<BossScoringData>(MessageEvent.ContextEvent.BossScoring, UpdateScore);
            MessageManager.Instance.GetManager(MessageArea.Process)
                .Subscribe<BattleSite>(MessageEvent.ProcessEvent.OnRoundEnd,UpdateRoundNumber);
            MessageManager.Instance.GetManager(MessageArea.Context)
                .Subscribe<BattleSite>(MessageEvent.ContextEvent.CharaDead, UpdateDeaths);
        }
    }

}
