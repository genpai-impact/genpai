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
    /// ������Ϸ���ؼ�
    /// </summary>
    public class EndGameBannerDisplay : MonoSingleton<EndGameBannerDisplay>, IMessageReceiveHandler
    {
        [Tooltip("�����˺���")]
        [FormerlySerializedAs("LeftScoreText")] public Text leftScoreText;
        [Tooltip("���˺���")]
        [FormerlySerializedAs("TotalScoreText")] public Text totalScoreText;
        [Tooltip("����������")]
        [FormerlySerializedAs("DeathsText")] public Text deathsText;
        [Tooltip("�����غ���")]
        [FormerlySerializedAs("RoundNumberText")] public Text roundNumberText;
        [Tooltip("�˺�����ֵ")]
        [FormerlySerializedAs("DamegebarImage")] public Image damegebarImage;

        [FormerlySerializedAs("LeftScore")] public int leftScore=0;
        [FormerlySerializedAs("TotalScore")] public int totalScore=0;
        [FormerlySerializedAs("Deaths")] public int deaths=0;
        [FormerlySerializedAs("RoundNumber")] public int roundNumber=0;

        /// <summary>
        /// ��ʼ�����
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
        /// �����������
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
        /// �����˺���
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
        /// ���»غ���
        /// </summary>
        /// <param name="site"></param>
        //�غ������ܻ���Щ�쳣����
        public void UpdateRoundNumber(BattleSite site)
        {
            if (site == BattleSite.P1) roundNumber++;
            UpdateDisplay();
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="site"></param>
        public void UpdateDeaths(BattleSite site)
        {
            if (site == BattleSite.P1) deaths++;
            UpdateDisplay();
        }
        /// <summary>
        /// Message���ġ����ڻ�ȡ��Ϸ��Ϣ��
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
